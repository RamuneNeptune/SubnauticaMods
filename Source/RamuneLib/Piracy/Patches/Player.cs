

namespace RamuneLib.Piracy.Patches
{
    public static class PlayerPatch
    {
        public static void Awake()
        {
            CoroutineHost.StartCoroutine(DisplayMessage());
        }


        public static void Update()
        {
            // nighty night

            DayNightCycle.main._dayNightSpeed = 0f;

            DayNightCycle.main.skipTimeMode = false;

            DayNightCycle.main.timePassedAsDouble = 1200;

            DayNightCycle.main.UpdateAtmosphere();


            // bloom

            UwePostProcessingManager.currentProfile.bloom.enabled = true;

            var bloom = UwePostProcessingManager.currentProfile.bloom.settings;

            bloom.lensDirt.intensity = 11f;

            bloom.bloom.intensity = 9f;

            UwePostProcessingManager.currentProfile.bloom.settings = bloom;


            // motion blur

            UwePostProcessingManager.currentProfile.motionBlur.enabled = true;

            var motionBlur = UwePostProcessingManager.currentProfile.motionBlur.settings;

            motionBlur.shutterAngle = 360f;

            motionBlur.sampleCount = 32;

            motionBlur.frameBlending = 1f;

            UwePostProcessingManager.currentProfile.motionBlur.settings = motionBlur;
        }


        /// <summary>
        /// Display a list of messages from on-screen dedicated to pirates from community members consistently.
        /// </summary>
        /// <returns></returns>
        public static IEnumerator DisplayMessage()
        {
            while(true)
            {
                yield return new WaitForSeconds(1f);
                Screen.Message(PiracyMessages);
            }
        }


        /// <summary>
        /// A list of messages from fellow Subnautica Modding members dedicated to pirates
        /// </summary>
        public static string PiracyMessages =
@"<color=#ffba1d><b>LeviathanKraken</b> says:</color> Monkey D. Luffy approves
<color=#ffba1d><b>KooKoo</b> says:</color> you scallywag!!
<color=#ffba1d><b>Dreamanchik</b> says:</color> ⚠ goober
<color=#ffba1d><b>Unknown</b> says:</color> Your mother
<color=#ffba1d><b>Cookie</b> says:</color> Hands off my booty!
<color=#ffba1d><b>Al-An</b> says:</color> ▖━┏┃▜┫┛";
    }
}