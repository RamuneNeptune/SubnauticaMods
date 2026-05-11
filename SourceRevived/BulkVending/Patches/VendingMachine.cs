

namespace Ramune.BulkVending.Patches
{
    [HarmonyPatch(typeof(VendingMachine))]
    public static class VendingMachinePatches
    {
        public static float Cooldown => BulkVending.config.SecondsPerSnack * (BulkVending.config.SnackAddsTime ? BulkVending.config.SnacksPerUse : 1);


        [HarmonyPatch(nameof(VendingMachine.OnHover)), HarmonyPrefix]
        public static bool OnHover(VendingMachine __instance, HandTargetEventData eventData)
        {
            var totalPowerCost = Mathf.Max(0f, BulkVending.config.SnackPowerCostPerItem) * BulkVending.config.SnacksPerUse;
            var remainingCooldown = Mathf.CeilToInt(Mathf.Max(0f, __instance.timeLastUse + Cooldown - Time.time));

            if(__instance.powerRelay == null || !__instance.powerRelay.IsPowered() || BulkVending.config.SnackRequiresPower && totalPowerCost > 0f && __instance.powerRelay.GetPower() < totalPowerCost)
                return false;

            if(remainingCooldown > 0)
            {
                HandReticle.main.SetText(HandReticle.TextType.Hand, Language.main.GetFormat("availablein".LangKeyAbbr(), remainingCooldown), false);
                HandReticle.main.SetIcon(HandReticle.IconType.HandDeny);
                return false;
            }

            if(GameInput.GetButtonDown(BulkVending.IncreaseAmount))
                BulkVending.config.SnacksPerUse++;

            if(GameInput.GetButtonDown(BulkVending.DecreaseAmount) && BulkVending.config.SnacksPerUse > 1)
                BulkVending.config.SnacksPerUse--;

            HandReticle.main.SetText(HandReticle.TextType.Hand, Language.main.GetFormat("snackusetext".LangKeyAbbr(), BulkVending.config.SnacksPerUse, Mathf.RoundToInt(Cooldown), GameInput.FormatButton(GameInput.Button.LeftHand)), false);
            HandReticle.main.SetText(HandReticle.TextType.HandSubscript, Language.main.GetFormat("subtext".LangKeyAbbr(), GameInput.FormatButton(BulkVending.IncreaseAmount), GameInput.FormatButton(BulkVending.DecreaseAmount)), false);
            HandReticle.main.SetIcon(HandReticle.IconType.Interact);
            return false;
        }


        [HarmonyPatch(nameof(VendingMachine.GetCanBeUsed)), HarmonyPostfix]
        public static void GetCanBeUsed(VendingMachine __instance, ref bool __result)
        {
            if(!__result)
                return;

            if(Time.time < __instance.timeLastUse + Cooldown)
            {
                __result = false;
                return;
            }

            if(BulkVending.config.SnackRequiresPower)
                __result = BulkVending.HasEnoughPower(__instance.powerRelay, BulkVending.config.SnackPowerCostPerItem * BulkVending.config.SnacksPerUse);
        }


        [HarmonyPatch(nameof(VendingMachine.OnUse)), HarmonyPrefix]
        public static bool OnUse(VendingMachine __instance)
        {
            if(Time.time < __instance.timeLastUse + Cooldown)
                return false;

            var relay = __instance.powerRelay;

            if(relay == null || !relay.IsPowered() || BulkVending.config.SnackRequiresPower && !BulkVending.TryConsumePower(relay, BulkVending.config.SnackPowerCostPerItem * BulkVending.config.SnacksPerUse))
                return false;

            __instance.timeLastUse = Time.time;

            for(int i = 0; i < BulkVending.config.SnacksPerUse; i++)
                CraftData.AddToInventory(__instance.snacks.GetRandom(), 1, noMessage: false, spawnIfCantAdd: false);

            if(__instance.useSound != null)
                FMODUWE.PlayOneShot(__instance.useSound, __instance.transform.position);

            return false;
        }
    }
}