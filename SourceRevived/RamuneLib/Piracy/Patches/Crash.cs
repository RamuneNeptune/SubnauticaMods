

namespace RamuneLib.Piracy.Patches
{
    internal static class CrashPatch
    {
        internal static void Detonate(Crash __instance)
        {
            if(UnityEngine.Random.value > 0.3f)
            {
                return;
            }
            
            CoroutineHost.StartCoroutine(Piracy.SpawnCrashfish(__instance.transform.position));
        }
    }
}