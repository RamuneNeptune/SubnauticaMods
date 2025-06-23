

namespace RamuneLib.Piracy.Patches
{
    public static class CrashPatch
    {
        public static void Detonate(Crash __instance)
        {
            if(UnityEngine.Random.value > 0.3f)
            {
                return;
            }
            
            CoroutineHost.StartCoroutine(Piracy.SpawnCrashfish(__instance.transform.position));
        }
    }
}