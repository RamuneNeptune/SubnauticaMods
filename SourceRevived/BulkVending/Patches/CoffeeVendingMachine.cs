

namespace Ramune.BulkVending.Patches
{
    [HarmonyPatch(typeof(CoffeeVendingMachine))]
    public static class CoffeeVendingMachinePatches
    {
        public static readonly Dictionary<CoffeeVendingMachine, int> RemainingBulkCoffees = [];

        public static float Cooldown => BulkVending.config.SecondsPerCoffee * (BulkVending.config.CoffeeAddsTime ? BulkVending.config.CoffeesPerUse : 1);


        [HarmonyPatch(nameof(CoffeeVendingMachine.OnDisable)), HarmonyPostfix]
        public static void OnDisable(CoffeeVendingMachine __instance)
        {
            RemainingBulkCoffees.Remove(__instance);
        }


        [HarmonyPatch(nameof(CoffeeVendingMachine.OnHover)), HarmonyPrefix]
        public static bool OnHover(CoffeeVendingMachine __instance)
        {
            var totalPowerCost = BulkVending.config.CoffeePowerCostPerItem * BulkVending.config.CoffeesPerUse;
            var canUse = BulkVending.config.CoffeesPerUse >= 2 ? Time.time > __instance.timeLastUseSlot1 + Cooldown : Time.time > __instance.timeLastUseSlot1 + Cooldown || Time.time > __instance.timeLastUseSlot2 + Cooldown;
            var remainingCooldown = BulkVending.config.CoffeesPerUse >= 2
                ? Mathf.CeilToInt(Mathf.Max(0f, __instance.timeLastUseSlot1 + Cooldown - Time.time))
                : Mathf.Min(
                    Mathf.CeilToInt(Mathf.Max(0f, __instance.timeLastUseSlot1 + Cooldown - Time.time)),
                    Mathf.CeilToInt(Mathf.Max(0f, __instance.timeLastUseSlot2 + Cooldown - Time.time)));

            if(!__instance.enabled || __instance.powerRelay == null || !__instance.powerRelay.IsPowered() || BulkVending.config.CoffeeRequiresPower && totalPowerCost > 0f && __instance.powerRelay.GetPower() < totalPowerCost)
                return false;

            if(!canUse && remainingCooldown > 0)
            {
                HandReticle.main.SetText(HandReticle.TextType.Hand, Language.main.GetFormat("availablein".LangKeyAbbr(), remainingCooldown), false);
                HandReticle.main.SetIcon(HandReticle.IconType.HandDeny);
                return false;
            }

            if(!canUse)
                return false;

            HandReticle.main.SetText(HandReticle.TextType.Hand, Language.main.GetFormat("coffeeusetext".LangKeyAbbr(), BulkVending.config.CoffeesPerUse, Mathf.RoundToInt(Cooldown), GameInput.FormatButton(GameInput.Button.LeftHand)), false);
            HandReticle.main.SetText(HandReticle.TextType.HandSubscript, string.Empty, false);
            HandReticle.main.SetIcon(HandReticle.IconType.Interact);
            return false;
        }


        [HarmonyPatch(nameof(CoffeeVendingMachine.OnMachineUse)), HarmonyPrefix]
        public static bool OnMachineUse(CoffeeVendingMachine __instance)
        {
            var relay = __instance.powerRelay;

            if(!__instance.enabled || relay == null || !relay.IsPowered())
                return false;

            if(BulkVending.config.CoffeesPerUse >= 2)
            {
                if(Time.time <= __instance.timeLastUseSlot1 + Cooldown || BulkVending.config.CoffeeRequiresPower && !BulkVending.TryConsumePower(relay, BulkVending.config.CoffeePowerCostPerItem * BulkVending.config.CoffeesPerUse))
                    return false;

                __instance.vfxController.Play(0);
                __instance.waterSoundSlot1.Play();
                __instance.timeLastUseSlot1 = Time.time;
                RemainingBulkCoffees[__instance] = BulkVending.config.CoffeeAddsTime ? BulkVending.config.CoffeesPerUse - 1 : 0;
                __instance.Invoke(nameof(CoffeeVendingMachine.SpawnCoffee), BulkVending.config.SecondsPerCoffee);
                return false;
            }

            if(Time.time > __instance.timeLastUseSlot1 + Cooldown)
            {
                if(BulkVending.config.CoffeeRequiresPower && !BulkVending.TryConsumePower(relay, BulkVending.config.CoffeePowerCostPerItem * BulkVending.config.CoffeesPerUse))
                    return false;

                __instance.vfxController.Play(0);
                __instance.waterSoundSlot1.Play();
                __instance.timeLastUseSlot1 = Time.time;
                __instance.Invoke(nameof(CoffeeVendingMachine.SpawnCoffee), BulkVending.config.SecondsPerCoffee);
            }
            else if(Time.time > __instance.timeLastUseSlot2 + Cooldown)
            {
                if(BulkVending.config.CoffeeRequiresPower && !BulkVending.TryConsumePower(relay, BulkVending.config.CoffeePowerCostPerItem * BulkVending.config.CoffeesPerUse))
                    return false;

                __instance.vfxController.Play(1);
                __instance.waterSoundSlot2.Play();
                __instance.timeLastUseSlot2 = Time.time;
                __instance.Invoke(nameof(CoffeeVendingMachine.SpawnCoffee), BulkVending.config.SecondsPerCoffee);
            }

            return false;
        }


        [HarmonyPatch(nameof(CoffeeVendingMachine.SpawnCoffee)), HarmonyPrefix]
        public static bool SpawnCoffee(CoffeeVendingMachine __instance)
        {
            var localPlayer = Utils.GetLocalPlayer();
            var amountToSpawn = BulkVending.config.CoffeesPerUse >= 2 && !BulkVending.config.CoffeeAddsTime ? BulkVending.config.CoffeesPerUse : 1;

            if(localPlayer != null && Vector3.Distance(__instance.transform.position, localPlayer.transform.position) < __instance.maxDistToPlayer)
                CraftData.AddToInventory(TechType.Coffee, amountToSpawn, noMessage: false, spawnIfCantAdd: false);

            if(BulkVending.config.CoffeesPerUse >= 2 && BulkVending.config.CoffeeAddsTime && RemainingBulkCoffees.TryGetValue(__instance, out var remainingCoffees))
            {
                remainingCoffees--;

                if(remainingCoffees > 0)
                {
                    RemainingBulkCoffees[__instance] = remainingCoffees;
                    __instance.Invoke(nameof(CoffeeVendingMachine.SpawnCoffee), BulkVending.config.SecondsPerCoffee);
                }
                else RemainingBulkCoffees.Remove(__instance);
            }

            return false;
        }
    }
}