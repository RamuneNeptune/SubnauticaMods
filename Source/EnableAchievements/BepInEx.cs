

namespace Ramune.EnableAchievements
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class EnableAchievements : BaseUnityPlugin
    {
        public static EnableAchievements Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.EnableAchievements";
        public const string Name = "Enable Achievements";
        public const string Version = "3.0.0";

        public void Awake()
        {
            Initializer.Initialize(harmony, Logger, Name, Version);
        }
    }
}