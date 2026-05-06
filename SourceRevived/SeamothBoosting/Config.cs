

namespace Ramune.SeamothBoosting
{
    [Menu("SeamothBoosting")]
    public class Config : ConfigFile
    {
        [Button("Teleport closest Seamoth here", Tooltip = "Checks the distance of nearby Seamoths before teleporting the closest one to you")]
        public void TeleportSeamoth(ButtonClickedEventArgs _)
        {
            SeaMoth[] array = Object.FindObjectsOfType<SeaMoth>();
            bool flag = array.Length < 1;
            if(flag)
            {
                Screen.Warning("Could not find any Seamoths to teleport");
            }
            else
            {
                SeaMoth seaMoth = null;
                float num = float.MaxValue;
                string arg = "";
                foreach(SeaMoth seaMoth2 in array)
                {
                    float num2 = Vector3.Distance(seaMoth2.transform.position, Player.main.transform.position);
                    bool flag2 = num2 < num;
                    if(flag2)
                    {
                        num = num2;
                        seaMoth = seaMoth2;
                        arg = seaMoth2.vehicleName;
                    }
                }
                seaMoth.TeleportVehicle(Player.main.transform.position, MainCamera.camera.transform.rotation, false);
                Screen.Info(string.Format("Summoned [{0}] ({1}m)", arg, num.Clamp01()));
            }
        }


        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerCfg = false;

        [Slider(" • Boost speed multiplier", Format = "{0:0.0}x", DefaultValue = 1.5f, Min = 1f, Max = 10f, Step = 0.1f, Tooltip = "Changes are applied automatically", Order = 7)]
        public float speedMultiplier = 1.5f;

        [Slider(" • Boost energy usage multiplier", Format = "{0:0.0}x", DefaultValue = 2f, Min = 1f, Max = 10f, Step = 0.1f, Tooltip = "Changes are applied automatically", Order = 8)]
        public float energyMultiplier = 2f;

        [Toggle(" • Boost subtitle indicators", Tooltip = "Displays indicative subtitles when initially entering and exiting boost mode", Order = 9)]
        public bool boostSubtitles = true;


        [Toggle("<color=#ffc600>Engine SFX:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerEngine = false;

        [Toggle(" • Increase engine volume", Tooltip = "Increases the volume of the engine while boosting, serving as an audible cue", Order = 12)]
        public bool engineVolume = true;

        [Toggle(" • Increase engine pitch", Tooltip = "Increases the pitch of the engine while boosting, serving as an audible cue", Order = 13)]
        public bool enginePitch = true;

        [Slider(" • Engine volume multiplier", Format = "{0:F2}", DefaultValue = 1.425f, Min = 1f, Max = 3f, Step = 0.01f, Tooltip = "Changes are applied automatically", Order = 14)]
        public float volume = 1.42f;

        [Slider(" • Engine pitch multiplier", Format = "{0:F2}", DefaultValue = 1.15f, Min = 1f, Max = 3f, Step = 0.01f, Tooltip = "Changes are applied automatically", Order = 15)]
        public float pitch = 1.15f;


        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}