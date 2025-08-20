

namespace Ramune.RamunesCustomizedStorage
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class RamunesCustomizedStorage : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static RamunesCustomizedStorage Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.RamunesCustomizedStorage";
        public const string Name = "RamunesCustomizedStorage";
        public const string Version = "1.0.4";

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/RamunesCustomizedStorage/Version.json"))
                return;
            /*
            var inbox = new ModInbox("RamunesCustomizedStorage", true);

            ModMessageSystem.RegisterInbox(inbox);

            
            var reader = new BasicModMessageReader("RamunesCustomizedStorage", args =>
            {
                if(args.Length != 3 || args[0] is not string modName || args[1] is not Vector2 sizeVector || args[2] is not bool shouldRemoveVector)
                {
                    Logfile.Warning($"Recieved invalid mod message with args");
                    return;
                }

                Logfile.Warning($"Recieved args: \"{modName}\", \"{sizeVector}\", \"{shouldRemoveVector}\"");

                if(Patches.PDAPatch.SizeAdditions.TryGetValue(modName, out var vectorList))
                {
                    if(shouldRemoveVector)
                    {
                        vectorList.Remove(sizeVector);
                        Logfile.Warning($"Removed vector: {vectorList.Sum(v => v.y)}");
                    }
                    else if(!vectorList.Contains(sizeVector))
                    {
                        vectorList.Add(sizeVector);
                        Logfile.Warning($"Added vector: {vectorList.Sum(v => v.y)}");
                    }
                    else Logfile.Warning($"Did not add duplicate vector");
                }
                else
                {
                    Patches.PDAPatch.SizeAdditions[modName] = new List<Vector2>() { sizeVector };
                    Logfile.Warning($"Added vector: {Patches.PDAPatch.SizeAdditions[modName].Sum(v => v.y)}");
                }
            });
            
            inbox.AddMessageReader(reader);
            */

            CoroutineHost.StartCoroutine(CompatibilityPatchCheck());
        }


        public static bool ShouldPatchCompatibility = false;


        public static IEnumerator CompatibilityPatchCheck()
        {
            yield return PatchingUtils.WaitForChainloader();

            ShouldPatchCompatibility = Chainloader.PluginInfos.ContainsKey("sn.bagequipment.mod");

            Logfile.Info("Patching compatibility for sn.baqequipment.mod");
        }
    }
}