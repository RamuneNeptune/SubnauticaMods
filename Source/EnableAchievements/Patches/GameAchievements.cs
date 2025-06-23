

namespace Ramune.EnableAchievements.Patches
{
    [HarmonyPatch(typeof(GameAchievements))]
    public class GameAchievementsPatch
    {
        [HarmonyPatch(nameof(GameAchievements.Unlock))]
        public static bool Prefix(GameAchievements.Id id)
        {
            PlatformUtils.main.GetServices().UnlockAchievement(id);

            Logfile.Info($">> Unlocked '{id}' (if you already have this achievement that's fine, the game runs this code anyways)");

            return false;
        }
    }
}