

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
        public const string Version = "4.0.0";


        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/RamunesWorkbench/Version.json"))
                return;

            Buildables.RamunesWorkbench.Patch();

            var inbox = new ModInbox("RamunesWorkbench", true);

            ModMessageSystem.RegisterInbox(inbox);

            var reader = new BasicModMessageReader("RamunesWorkbench", args => CoroutineHost.StartCoroutine(Validate(args)));

            inbox.AddMessageReader(reader);
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
                    yield break;
                
                CraftHandler.AddTab(msg.Id ?? msg.TabName, msg.TabName, msg.Sprite, msg.StepsToTab);
            }
            else
            {
                if(msg.TechType != null)
                {
                    var techType = msg.TechType.Value;
                    CraftHandler.AddCraft(techType, msg.StepsToTab);
                    Patches.KnownTechPatch.ModdedTechTypeStrings.AddUnique(techType.AsString());
                }
                else if(!string.IsNullOrEmpty(msg.TechTypeString))
                {
                    CraftHandler.AddCraft(msg.TechTypeString, msg.StepsToTab);
                    Patches.KnownTechPatch.ModdedTechTypeStrings.AddUnique(msg.TechTypeString);
                }
                else
                {
                    Logfile.Error("Invalid AddCraftNode message (missing tech type)");
                }
            }

            yield break;
        }
    }
}