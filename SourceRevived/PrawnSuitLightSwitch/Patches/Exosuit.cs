

namespace Ramune.PrawnSuitLightSwitch.Patches
{
    [HarmonyPatch(typeof(Exosuit))]
    public static class ExosuitPatch
    {
        public static Light[] lights;

        public static FMODAsset lightOn = Utility.AudioUtils.GetFmodAsset("event:/sub/seamoth/seamoth_light_on");

        public static FMODAsset lightOff = Utility.AudioUtils.GetFmodAsset("event:/sub/seamoth/seamoth_light_off");

        public static bool on, sounds, subtitles;


        [HarmonyPatch(typeof(Exosuit), nameof(Exosuit.Start)), HarmonyPostfix]
        public static void Start(Exosuit __instance)
        {
            Light[] exosuitLights = __instance.gameObject.FindChild("lights_parent").GetComponentsInChildren<Light>(true);

            if(exosuitLights is not null)
                lights = exosuitLights;

            else
                Logfile.Error("Couldn't find lights for Exosuit");
        }


        [HarmonyPatch(typeof(Exosuit), nameof(Exosuit.Update)), HarmonyPostfix]
        public static void Update(Exosuit __instance)
        {
            if(Player.main.inExosuit && GameInput.GetButtonDown(PrawnSuitLightSwitch.ToggleLights) && !Cursor.visible)
            {
                on = !on;

                sounds = PrawnSuitLightSwitch.config.sounds;
                subtitles = PrawnSuitLightSwitch.config.debug;

                if(sounds)
                    FMODUWE.PlayOneShot(on ? lightOn : lightOff, __instance.transform.position);

                if(subtitles)
                    Subtitles.Add(on ? "disablingsubtitle".LangKeyAbbr() : "enablingsubtitle".LangKeyAbbr());

                lights.ForEach(li => li.enabled = !on);

                __instance.hasInitStrings = false;
            }
        }


        [HarmonyPatch(typeof(Exosuit), nameof(Exosuit.UpdateUIText)), HarmonyPrefix]
        public static bool UpdateUIText(Exosuit __instance, bool hasPropCannon)
        {
            if(PrawnSuitLightSwitch.config.ui is false)
                return true;

            if(!__instance.hasInitStrings || __instance.lastHasPropCannon != hasPropCannon)
            {
                __instance.sb.Length = 0;
                __instance.sb.AppendLine(LanguageCache.GetButtonFormat("PressToExit", GameInput.Button.Exit));

                __instance.sb.AppendLine(LanguageCache.GetButtonFormat(on ? "ramune.psls.turnlightson" : "ramune.psls.turnlightsoff", GameInput.Button.CyclePrev));

                if(hasPropCannon)
                    __instance.sb.AppendLine(LanguageCache.GetButtonFormat("PropulsionCannonToRelease", GameInput.Button.AltTool));

                __instance.lastHasPropCannon = hasPropCannon;
                __instance.uiStringPrimary = __instance.sb.ToString();
            }

            HandReticle.main.SetTextRaw(HandReticle.TextType.Use, __instance.uiStringPrimary);
            HandReticle.main.SetTextRaw(HandReticle.TextType.UseSubscript, string.Empty);
            __instance.hasInitStrings = true;

            return false;
        }
    }
}