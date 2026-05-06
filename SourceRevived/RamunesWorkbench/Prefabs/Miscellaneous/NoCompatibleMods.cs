

namespace Ramune.RamunesWorkbench.Prefabs.Miscellaneous
{
    public static class NoCompatibleMods
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefab("NoCompatibleMods", "No compatible mods", "Install <color=#ffc600>compatible mods</color> for items to appear in this workbench.")
            .WithJsonRecipe("NoCompatibleMods")
            .WithAutoUnlock();

        public static void Register()
        {
            Prefab.SetGameObject(new CloneTemplate(Prefab.Info, TechType.Aerogel));
            Prefab.Register();
        }
    }
}