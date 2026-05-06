

namespace Ramune.TableCoralMultiplier.Patch
{
    [HarmonyPatch(typeof(SpawnOnKill))]
    public static class SpawnOnKillPatch
    {
        [HarmonyPatch(nameof(SpawnOnKill.OnKill)), HarmonyPostfix]
        public static void OnKill(SpawnOnKill __instance)
        {
            if(!__instance.prefabToSpawn.name.StartsWith("JeweledDiskPiece"))
                return;

            for(int i = 0; i < TableCoralMultiplier.config.TableCoralToSpawn - 1; i++)
            {
                var go = Object.Instantiate(__instance.prefabToSpawn, __instance.transform.position, __instance.transform.rotation);

                if(__instance.randomPush)
                {
                    var rb = go.GetComponent<Rigidbody>();

                    if(rb)
                        rb.AddForce(UnityEngine.Random.onUnitSphere * 1.4f, ForceMode.Impulse);
                }
            }
        }
    }
}