

namespace Ramune.ReaperGoldSkin
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class ReaperGoldSkin : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static ReaperGoldSkin Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.ReaperGoldSkin";
        public const string Name = "ReaperGoldSkin";
        public const string Version = "5.0.0";

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/SourceRevived/ReaperGoldSkin/Version.json"))
                return;

            StartCoroutine(ModifyReaperPrefabAsync());
        }


        public IEnumerator ModifyReaperPrefabAsync()
        {
            var task = GetPrefabForTechTypeAsync(TechType.ReaperLeviathan);

            yield return task;

            var reaperPrefab = task.GetResult();

            if(!reaperPrefab.TryGetComponentInChildren<Renderer>(out var renderer))
                yield break;

            renderer.SetTexture(RamuneLib.Extensions.RendererExtensions.TextureType.Main, ImageUtils.GetTexture("Reaper.Texture"), true);
            renderer.SetTexture(RamuneLib.Extensions.RendererExtensions.TextureType.Specular, ImageUtils.GetTexture("Reaper.Texture"), true);
            renderer.SetTexture(RamuneLib.Extensions.RendererExtensions.TextureType.Emissive, ImageUtils.GetTexture("Reaper.Illum"), true);
            renderer.SetTexture(RamuneLib.Extensions.RendererExtensions.TextureType.Illum, ImageUtils.GetTexture("Reaper.Illum"), true);
            renderer.SetGlowStrength(0.5f, true);
        }
    }
}