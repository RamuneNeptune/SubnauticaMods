

namespace Ramune.SeaglideBoosting.Patches
{
    [HarmonyPatch(typeof(Seaglide))]
    public static class SeaglidePatch
    {
        public static List<float> seamothDefaults = [];

        public static bool isBoosting, wasBoosting;

        public static TechType currentTechType;


        [HarmonyPatch(nameof(SeaMoth.OnPilotModeBegin)), HarmonyPrefix]
        public static void OnPilotModeBegin(SeaMoth __instance)
        {
            seamothDefaults.Clear();
            seamothDefaults.Add(__instance.enginePowerConsumption);
            seamothDefaults.Add(__instance.forwardForce);
            seamothDefaults.Add(__instance.backwardForce);
            seamothDefaults.Add(__instance.sidewardForce);
        }


        [HarmonyPatch(nameof(SeaMoth.Update)), HarmonyPostfix]
        public static void Update(SeaMoth __instance)
        {
            if(!__instance.energyInterface.hasCharge)
                return;

            isBoosting = GameInput.GetButtonHeld(SeamothBoosting.SeamothBoosting.Boost);

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


        public static void ApplyBoostChanges(SeaMoth seamoth, bool isBoosting)
        {
            if(!Player.main.playerController)
                return;

            var boostMultiplier = SeamothBoosting.SeamothBoosting.config.speedMultiplier;

            var pitch = boostMultiplier < 1f ? 0.87f : 1.1f;

            seamoth.animator.speed = isBoosting ? boostMultiplier : 1f;

            var engineRpmSfx = seamoth.engine.engineRpmSFX.GetEventInstance();
            engineRpmSfx.setPitch(isBoosting ? pitch : 1f);
            engineRpmSfx.setVolume(isBoosting ? 1.1f : 1f);

            seam
        }
    }
}