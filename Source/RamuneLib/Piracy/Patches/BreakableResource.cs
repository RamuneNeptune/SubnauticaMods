

namespace RamuneLib.Piracy.Patches
{
    public static class BreakableResourcePatch
    {
        public static void BreakIntoResources(BreakableResource __instance)
        {
            ///Outcrops occasionally spawn a crashfish when broken.
            if(UnityEngine.Random.value <= 0.1f)
                return;

            CoroutineHost.StartCoroutine(Piracy.SpawnCrashfish(__instance.transform.position));
        }
    }
}