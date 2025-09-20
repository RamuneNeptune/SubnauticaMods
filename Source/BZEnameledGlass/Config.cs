

namespace Ramune.BZEnameledGlass
{
    [Menu("BZEnameledGlass")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;

        [Button("Open recipe file")]
        public void OpenRecipeFile(ButtonClickedEventArgs _) => Process.Start(Path.Combine(Paths.RecipeFolder, "EnameledGlassClone.json"));
    }
}