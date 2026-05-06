

namespace Ramune.SeaglideBoosting.Patches
{
    [HarmonyPatch(typeof(Seaglide))]
    public static class SeaglidePatch
    {
        public static List<float> seaglideDefaults = [];

        public static bool isBoosting, wasBoosting;

        public static TechType currentTechType;


        [HarmonyPatch(nameof(Seaglide.OnDraw)), HarmonyPrefix]
        public static void OnDraw(Seaglide __instance)
        {
            currentTechType = __instance.pickupable.GetTechType();

            seaglideDefaults.Clear();
            seaglideDefaults.Add(Player.main.playerController.seaglideForwardMaxSpeed);
            seaglideDefaults.Add(Player.main.playerController.seaglideWaterAcceleration);
            seaglideDefaults.Add(Player.main.playerController.seaglideBackwardMaxSpeed);
        }


        [HarmonyPatch(nameof(Seaglide.Update)), HarmonyPostfix]
        public static void Update(Seaglide __instance)
        {
            if(currentTechType != TechType.Seaglide || !Player.main || Player.main.precursorOutOfWater || !Player.main.IsUnderwater() || !__instance.HasEnergy())
                return;

            isBoosting = GameInput.GetButtonHeld(SeaglideBoosting.Boost);

            if(isBoosting && !wasBoosting)
            {
                wasBoosting = true;
                ApplyBoostChanges(__instance, isBoosting);
            }
            else if(wasBoosting && !isBoosting)
            {
                wasBoosting = false;
                ApplyBoostChanges(__instance, isBoosting);
            }
        }


        [HarmonyPatch(nameof(Seaglide.UpdateEnergy)), HarmonyPrefix]
        public static bool UpdateEnergy(Seaglide __instance)
        {
            if(currentTechType != TechType.Seaglide)
                return true;

            if(!isBoosting)
                return true;

            if(__instance.activeState)
            {
                __instance.timeSinceUse += Time.deltaTime;
                if(__instance.timeSinceUse >= 1f)
                {
                    __instance.energyMixin.ConsumeEnergy(0.3f * SeaglideBoosting.config.energyMultiplier);
                    __instance.timeSinceUse -= 1f;
                }
            }
            return false;
        }


        public static void ApplyBoostChanges(Seaglide seaglide, bool isBoosting)
        {
            if(!Player.main.playerController)
                return;

            var boostMultiplier = SeaglideBoosting.config.boostMultiplier;

            var pitch = boostMultiplier < 1f ? 0.87f : 1.1f;

            seaglide.animator.speed = isBoosting ? boostMultiplier : 1f;

            var engineRpmSfx = seaglide.engineRPMManager.engineRpmSFX.GetEventInstance();
            engineRpmSfx.setPitch(isBoosting ? pitch : 1f);
            engineRpmSfx.setVolume(isBoosting ? 1.1f : 1f);

            Player.main.playerController.underWaterController.forwardMaxSpeed = isBoosting
                ? seaglideDefaults[0] * boostMultiplier
                : seaglideDefaults[0];

            Player.main.playerController.underWaterController.waterAcceleration = isBoosting
                ? seaglideDefaults[1] * boostMultiplier
                : seaglideDefaults[1];

            Player.main.playerController.underWaterController.backwardMaxSpeed = isBoosting
                ? seaglideDefaults[2] * boostMultiplier
                : seaglideDefaults[2];

            Player.main.playerController.underWaterController.strafeMaxSpeed = isBoosting
                ? seaglideDefaults[2] * boostMultiplier
                : seaglideDefaults[2];

            Player.main.playerController.underWaterController.verticalMaxSpeed = isBoosting
                ? seaglideDefaults[2] * boostMultiplier
                : seaglideDefaults[2];
        }
    }
}