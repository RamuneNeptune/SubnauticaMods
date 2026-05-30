

namespace RamuneLib.Piracy.Patches
{
    internal static class PlayerPatches
    {
        internal static void Awake()
        {
            CoroutineHost.StartCoroutine(DisplayMessage());
        }


        internal static void OnTakeDamage()
        {
            MainCameraControl.main.camShake = 0f;
            MainCameraControl.main.ShakeCamera(100f, 10f, MainCameraControl.ShakeMode.Sqrt, 1.38f);
        }


        internal static void Update()
        {
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
        internal static IEnumerator DisplayMessage()
        {
            while(true)
            {
                yield return new WaitForSeconds(1f);
                Screen.Message(string.Join("\n", PiracyMessages));
            }
        }


        /// <summary>
        /// A list of messages from fellow Subnautica Modding members dedicated to pirates
        /// </summary>
        internal static string[] PiracyMessages =
        [
@"
<b><color=#ffba1d>Noticeboard:</color></b>
<size=80%><color=#ffba1d><b>• RamuneNeptune</b> says:</color> Davy Jones sends his regards
<color=#ffba1d><b>• LeviathanKraken</b> says:</color> Monkey D. Luffy approves
<color=#ffba1d><b>• Aftersock</b> says:</color> You son of a motherless goat
<color=#ffba1d><b>• EgeK</b> says:</color> Now with treasure hunts!
<color=#ffba1d><b>• Cookie</b> says:</color> Hands off my booty
<color=#ffba1d><b>• Dreamanchik</b> says:</color> ⚠ goober
<color=#ffba1d><b>• Ray</b> says:</color> Shiver me timbers!!
<color=#ffba1d><b>• Unknown</b> says:</color> Your mother
<color=#ffba1d><b>• Al-An</b> says:</color> ▖━┏┃▜┫┛</size>"
        ];
    }
}