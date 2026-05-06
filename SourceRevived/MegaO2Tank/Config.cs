

namespace Ramune.MegaO2Tank
{
    [Menu("MegaO2Tank")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>", Order = 0)]
        public bool DividerCfg = false;

        [Slider("Tank oxygen capacity", Format = "{0:F1}", DefaultValue = 360f, Min = 180f, Max = 720f, Step = 10f, Tooltip = "Changes are applied on restart.", Order = 1)]
        public float oxygenCapacity = 360f;

        [Button("Close game (to apply changes)", Order = 2)]
        public void Close(ButtonClickedEventArgs _) => Application.Quit();

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>", Order = 3)]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it", Order = 4 )]
        public bool EnableThisMod = true;
    }   
}