

namespace RamuneLib.Piracy.Patches
{
    internal static class PoopPatch
    {
        internal static void Perform(Poop __instance, Creature creature, float time, float deltaTime)
        {
            if(!(time >= __instance.recourceSpawnTime))
                return;
            
            for(int i = 0; i < 29; i++)
                Object.Instantiate(__instance.recourcePrefab, __instance.recourceSpawnPoint.position, __instance.recourceSpawnPoint.rotation);
        }
    }
}