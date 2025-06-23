

namespace RamuneLib.Piracy.Patches
{
    public static class PlayerPatch
    {
        public static void Awake(Player __instance)
        {
            DayNightCycle.main._dayNightSpeed *= 0.75f;

            CoroutineHost.StartCoroutine(DisplayMessage());
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
<color=#ffba1d><b>RamuneNeptune</b> says:</color> 
<color=#ffba1d><b>KooKoo</b> says:</color> you scallywag!!
<color=#ffba1d><b>Dreamanchik</b> says:</color> ⚠ goober
<color=#ffba1d><b>Unknown</b> says:</color> Your mother
<color=#ffba1d><b>Cookie</b> says:</color> Hands off my booty!
<color=#ffba1d><b>Al-An</b> says:</color> ▖━┏┃▜┫┛";
    }
}