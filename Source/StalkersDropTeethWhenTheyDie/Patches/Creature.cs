

namespace Ramune.StalkersDropTeethWhenTheyDie.Patches
{
    [HarmonyPatch(typeof(Creature), nameof(Creature.OnKill))]
    public static class CreaturePatch
    {
        public static void Postfix(Creature __instance)
        {
            if(__instance.GetType() != typeof(Stalker))
                return;

            Stalker stalker = __instance.gameObject.GetComponent<Stalker>();

            if(stalker == null || stalker.toothPrefab == null || stalker.loseToothDropLocation == null)
                return;

            for(int i = 0; i < StalkersDropTeethWhenTheyDie.config.teethToDrop; i++)
            {
                GameObject gameObject = Object.Instantiate(stalker.toothPrefab);

                gameObject.transform.position = stalker.loseToothDropLocation.transform.position;

                gameObject.transform.rotation = stalker.loseToothDropLocation.transform.rotation;

                if(gameObject.activeSelf && stalker.isActiveAndEnabled)
                {
                    Collider[] componentsInChildren = gameObject.GetComponentsInChildren<Collider>();

                    for(int i_ = 0; i_ < componentsInChildren.Length; i_++)
                    {
                        Physics.IgnoreCollision(stalker.stalkerBodyCollider, componentsInChildren[i_]);
                    }
                }

                LargeWorldEntity.Register(gameObject);
            }
        }
    }
}