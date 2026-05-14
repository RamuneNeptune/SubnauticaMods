

namespace Ramune.PridenauticaRedux
{
    [Menu("PridenauticaRedux")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerCfg = false;

        [Toggle(" • Enable rainbow colors", Tooltip = "Requires a restart to take effect if you are already in-game")]
        public bool EnableRainbow = true;

        [Toggle(" • Enable bisexual colors", Tooltip = "Requires a restart to take effect if you are already in-game")]
        public bool EnableBisexual = false;

        [Toggle(" • Enable pansexual colors", Tooltip = "Requires a restart to take effect if you are already in-game")]
        public bool EnablePansexual = false;

        [Toggle(" • Enable lesbian colors", Tooltip = "Requires a restart to take effect if you are already in-game")]
        public bool EnableLesbian = false;

        [Toggle(" • Enable non-binary colors", Tooltip = "Requires a restart to take effect if you are already in-game")]
        public bool EnableNonBinary = false;

        [Toggle(" • Enable transgender colors", Tooltip = "Requires a restart to take effect if you are already in-game")]
        public bool EnableTransgender = false;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;

        public IEnumerable<string> EnabledTextures
        {
            get
            {
                if(EnableRainbow)
                    yield return "Rainbow";

                if(EnableBisexual)
                    yield return "Bisexual";

                if(EnablePansexual)
                    yield return "Pansexual";

                if(EnableLesbian)
                    yield return "Lesbian";

                if(EnableNonBinary)
                    yield return "NonBinary";

                if(EnableTransgender)
                    yield return "Transgender";
            }
        }
    }
}