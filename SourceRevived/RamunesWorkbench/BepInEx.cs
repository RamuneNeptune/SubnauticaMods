

namespace Ramune.RamunesWorkbench
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class RamunesWorkbench : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static RamunesWorkbench Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.RamunesWorkbench";
        public const string Name = "RamunesWorkbench";
        public const string Version = "5.0.0";

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/SourceRevived/RamunesWorkbench/Version.json"))
                return;

            LanguageHandler.RegisterLocalizationFolder();

            Prefabs.Buildables.RamunesWorkbench.Register();

            ModMessageUtils.RegisterGlobalInbox("RamunesWorkbench", args => CoroutineHost.StartCoroutine(Validate(args)));

            Prefabs.Miscellaneous.NoCompatibleMods.Register();

            SceneUtils.RegisterOnMainLoaded(() =>
            {
                if(craftNodesAdded > 0)
                    return;

                RamunesWorkbenchUtils.AddCraftNode(Prefabs.Miscellaneous.NoCompatibleMods.Prefab.Info.TechType);
            });
        }


        public class WorkbenchMessage
        {
            public bool IsTab { get; set; }

            public string? Id { get; set; }

            public string? TabName { get; set; }

            public Sprite? Sprite { get; set; }

            public string? TechTypeString { get; set; }

            public TechType? TechType { get; set; }

            public string[]? StepsToTab { get; set; } = [];
        }


        public static int craftNodesAdded = 0;


        public static IEnumerator Validate(object[] args)
        {
            while(!CraftHandler.IsInitialized)
                yield return null;

            if(args == null || args.Length != 7)
                yield break;

            var msg = new WorkbenchMessage
            {
                IsTab = (bool)args[0],
                Id = (string)args[1],
                TabName = (string)args[2],
                Sprite = (Sprite)args[3],
                TechTypeString = (string)args[4],
                TechType = args[5] is TechType tt ? tt : null,
                StepsToTab = (string[])args[6],
            };

            if(msg.IsTab)
            {
                if(string.IsNullOrEmpty(msg.TabName) || msg.Sprite == null)
                {
                    Logfile.Error("Received invalid AddTabNode message (missing TabName or Sprite)");
                    yield break;
                }

                CraftHandler.AddTab(msg.Id ?? msg.TabName, msg.TabName, msg.Sprite, msg.StepsToTab);
                Logfile.Info($"Added tab '{msg.Id ?? msg.TabName}'");
            }
            else
            {
                var stepsToTab = msg.StepsToTab == null ? "" : $" to: {string.Join(" -> ", msg.StepsToTab)}";

                if(msg.TechType != null)
                {
                    var techType = msg.TechType.Value;
                    CraftHandler.AddCraft(techType, msg.StepsToTab);
                    Patches.KnownTechPatch.ModdedTechTypeStrings.AddUnique(techType.AsString());
                    Logfile.Info($"Added craft '{techType}'{stepsToTab}");
                    craftNodesAdded++;
                }
                else if(!string.IsNullOrEmpty(msg.TechTypeString))
                {
                    CraftHandler.AddCraft(msg.TechTypeString, msg.StepsToTab);
                    Patches.KnownTechPatch.ModdedTechTypeStrings.AddUnique(msg.TechTypeString);
                    Logfile.Info($"Added craft '{msg.TechTypeString}'{stepsToTab}");
                    craftNodesAdded++;
                }
                else
                {
                    Logfile.Error("Received invalid AddCraftNode message (missing TechType)");
                }
            }

            yield break;
        }
    }
}