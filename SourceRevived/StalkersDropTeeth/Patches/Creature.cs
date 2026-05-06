

namespace Ramune.StalkersDropTeeth.Patches
{
    [HarmonyPatch(typeof(Creature), nameof(Creature.OnKill))]
    public static class CreaturePatch
    {
        public static void Postfix(Creature __instance)
        {
            if(__instance is not Stalker stalker)
                return;

            for(int i = 0; i < StalkersDropTeeth.config.TeethToDrop; i++)
            {

                GameObject gameObject = Object.Instantiate(stalker.toothPrefab);
                gameObject.transform.position = stalker.loseToothDropLocation.transform.position;
                gameObject.transform.rotation = stalker.loseToothDropLocation.transform.rotation;

                if(gameObject.activeSelf && stalker.isActiveAndEnabled)
                {
                    Collider[] componentsInChildren = gameObject.GetComponentsInChildren<Collider>();

                    for(int ii = 0; ii < componentsInChildren.Length; ii++)
                        Physics.IgnoreCollision(stalker.stalkerBodyCollider, componentsInChildren[ii]);
                }

                LargeWorldEntity.Register(gameObject);
            }
        }
    }
}