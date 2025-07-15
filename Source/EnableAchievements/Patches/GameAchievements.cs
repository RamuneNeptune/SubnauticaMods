

namespace Ramune.EnableAchievements.Patches
{
    [HarmonyPatch(typeof(GameAchievements))]
    public class GameAchievementsPatch
    {
        [HarmonyPatch(nameof(GameAchievements.Unlock))]
        public static bool Prefix(GameAchievements.Id id)
        {
            PlatformUtils.main.GetServices().UnlockAchievement(id);

            Logfile.Info($"Unlocked achievement ID: {id}");

            Screen.Info($"Unlocked achievement ID: {id}");

            return false;
        }
    }
}