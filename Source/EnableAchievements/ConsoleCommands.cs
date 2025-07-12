

namespace Ramune.EnableAchievements
{
    public static class ConsoleCommands
    {
        [ConsoleCommand("resetachievements")]
        public static void CommandResetAchievements()
        {
            PlatformUtils.main?.services.ResetAchievements();

            Screen.Info("Reset achievements");
        }

        [ConsoleCommand("unlockachievement")]
        public static void CommandUnlockAchievement(string id)
        {
            if(Enum.TryParse<GameAchievements.Id>(id, true, out var achievementId))
            {
                PlatformUtils.main?.services.UnlockAchievement(achievementId);

                Screen.Info($"Unlocked achievement ID: {id}");
            }
            else
            {
                Screen.Error($"Invalid achievement ID: {id}\nMust be one of the following:\n - {string.Join("\n - ", Enum.GetNames(typeof(GameAchievements.Id)))}");
            }
        }
    }
}