﻿

namespace Ramune.TableCoralMultiplier.Patches
{
    [HarmonyPatch(typeof(SpawnOnKill), nameof(SpawnOnKill.OnKill))]
    public static class SpawnOnKillPatch
    {
        public static void Postfix(SpawnOnKill __instance)
        {
            if(!__instance.prefabToSpawn.name.StartsWith("JeweledDiskPiece")) 
                return;

            float toSpawn = TableCoralMultiplier.config.TableCoralToSpawn;

            for(int i = 0; i < toSpawn - 1; i++)
            {
                var go = GameObject.Instantiate(__instance.prefabToSpawn, __instance.transform.position, __instance.transform.rotation);

                if(__instance.randomPush)
                {
                    go.GetComponent<Rigidbody>()?.AddForce(UnityEngine.Random.onUnitSphere * 1.4f, ForceMode.Impulse);
                }
            }
        }
    }
}