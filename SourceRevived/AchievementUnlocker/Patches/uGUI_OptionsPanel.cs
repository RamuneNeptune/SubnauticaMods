

namespace Ramune.AchievementUnlocker.Patches
{
    [HarmonyPatch(typeof(uGUI_OptionsPanel))]
    public static class SimpleOptionsPatch
    {
        [HarmonyPatch(nameof(uGUI_OptionsPanel.AddTabs))]
        [HarmonyPostfix]
        public static void AddTabs(uGUI_OptionsPanel __instance)
        {
            int tabIndex = __instance.AddTab("tabname".LangKeyAbbr());

            __instance.AddHeading(tabIndex, "headerlabel".LangKeyAbbr());

            __instance.AddHeading(tabIndex, "subtext".LangKeyAbbr());

            __instance.AddHeading(tabIndex, " ");

            __instance.AddButton(tabIndex, "unlockall".LangKeyAbbr(), () =>
            {
                foreach(GameAchievements.Id id in Enum.GetValues(typeof(GameAchievements.Id)))
                {
                    var idString = Regex.Replace(id.ToString(), "(\\B[A-Z])", " $1");

                    if(idString.Equals("None"))
                        continue;

                    PlatformUtils.main.GetServices().UnlockAchievement(id);
                    Logfile.Info($"Unlocked achievement with ID: {id}");
                    Screen.Message(string.Format("unlocktext".LangKeyAbbr(), idString));
                }
            });

            __instance.AddButton(tabIndex, "resetall".LangKeyAbbr(), () =>
            {
                PlatformUtils.main.GetServices().ResetAchievements();

                foreach(GameAchievements.Id id in Enum.GetValues(typeof(GameAchievements.Id)))
                {
                    var idString = Regex.Replace(id.ToString(), "(\\B[A-Z])", " $1");

                    if(idString.Equals("None"))
                        continue;

                    Logfile.Info($"Reset achievement with ID: {id}");
                    Screen.Message(string.Format("resettext".LangKeyAbbr(), idString));
                }
            });

            __instance.AddHeading(tabIndex, " ");

            foreach(GameAchievements.Id id in Enum.GetValues(typeof(GameAchievements.Id)))
            {
                var idString = Regex.Replace(id.ToString(), "(\\B[A-Z])", " $1");

                if(idString.Equals("None"))
                    continue;

                __instance.AddButton(tabIndex, idString, () =>
                {
                    PlatformUtils.main.GetServices().UnlockAchievement(id);
                    Logfile.Info($"Unlocked achievement with ID: {id}");
                    Screen.Message(string.Format("unlocktext".LangKeyAbbr(), idString));
                });
            }
        }
    }
}