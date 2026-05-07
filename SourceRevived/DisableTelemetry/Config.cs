

namespace Ramune.DisableTelemetry
{
    [Menu("DisableTelemetry")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerCfg = false;

        [Toggle(" • Allow special cosmetic items check (<color=#ffc600>dolls</color>, <color=#ffc600>hull plates</color>, etc.)", Tooltip = "Requires a restart to take effect, but allows the request made to check whether you own any special items. Such as the <color=#ffc600>markiplier doll</color> or <color=#ffc600>jacksepticeye tank</color>")]
        public bool AllowSpecialItemCheck = false;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}