

namespace Ramune.FlareDurationIndicator
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class FlareDurationIndicator : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static FlareDurationIndicator Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.FlareDurationIndicator";
        public const string Name = "FlareDurationIndicator";
        public const string Version = "5.0.0";

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/SourceRevived/FlareDurationIndicator/Version.json"))
                return;

            StartCoroutine(ModifyFlarePrefabAsync());
        }


        public IEnumerator ModifyFlarePrefabAsync()
        {
            var task = GetPrefabForTechTypeAsync(TechType.Flare);

            yield return task;

            var flarePrefab = task.GetResult();

            if(flarePrefab == null)
                yield break;

            flarePrefab.EnsureComponent<Monos.FlareEnergy>();

            var energyMixin = Utility.PrefabUtils.AddEnergyMixin(flarePrefab, "FlareEnergySlot", TechType.Battery, [TechType.Battery]);

            CoroutineHost.StartCoroutine(energyMixin.SpawnDefaultAsync(100f, DiscardTaskResult<bool>.Instance));

        }
    }
}