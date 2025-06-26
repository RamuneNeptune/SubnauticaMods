

namespace Ramune.EasyOutcrops
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class EasyOutcrops : BaseUnityPlugin
    {
        public static EasyOutcrops Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.EasyOutcrops";
        public const string Name = "EasyOutcrops";
        public const string Version = "1.0.0";

        public void Awake()
        {
            Initializer.Initialize(harmony, Logger, Name, Version);
        }
    }
}