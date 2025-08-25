

namespace Ramune.EquippableStorage
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class EquippableStorage : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static EquippableStorage Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.EquippableStorage";
        public const string Name = "EquippableStorage";
        public const string Version = "1.0.0";

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/EquippableStorage/Version.json"))
                return;

            LanguageHandler.RegisterLocalizationFolder();

            if(!EnumHandler.TryAddEntry<EquipmentType>("EquippableStorage", out var equipmentType))
            {
                Logfile.Fatal("Failed to add EquipmentType entry for \"EquippableStorage\"");
                Logfile.Fatal("The mod will not be loaded");
                return;
            }

            Equipment.slotMapping.Add("EquippableStorage1", equipmentType);
            Equipment.slotMapping.Add("EquippableStorage2", equipmentType);
            Equipment.slotMapping.Add("EquippableStorage3", equipmentType);
            
            for(int ii = 1; ii < config.backpacksToGenerate + 1; ii++)
            {
                int i = ii;

                var id = $"eq.backpack.{i}.id".LangKey();
                var name = $"eq.backpack.{i}.name".LangKey();
                var desc = $"eq.backpack.{i}.desc".LangKey();

                IDs.Add(id, i);

                var prefab = new CustomPrefab(id, name, desc, ImageUtils.GetSprite(TechType.LuggageBag/*$"eq.backpack.{i}.id".LangKey()*/))
                    .WithJsonRecipe(id, CraftTree.Type.Fabricator, CraftTreeHandler.Paths.FabricatorEquipment)
                    .WithEquipmentAndQuickSlotType(equipmentType, QuickSlotType.None)
                    .WithAutoUnlock()
                    .WithSize(2, 2);

                var clone = new CloneTemplate(prefab.Info, TechType.LuggageBag)
                {
                    ModifyPrefab = go =>
                    {
                        DestroyImmediate(go.GetComponentInChildren<PickupableStorage>());
                        DestroyImmediate(go.GetComponentInChildren<PlaceTool>());
                        Destroy(go.GetComponentInChildren<StorageContainer>());

                        VFXFabricating vfxfabricating = go.FindChild("model").AddComponent<VFXFabricating>();
                        vfxfabricating.localMinY = -0.1f;
                        vfxfabricating.localMaxY = 0.4f;
                        vfxfabricating.posOffset = new Vector3(0f, 0f, 0f);
                        vfxfabricating.eulerOffset = new Vector3(0f, 0f, 0f);
                        vfxfabricating.scaleFactor = 0.6f;
                    }
                };

                prefab.SetGameObject(clone);
                prefab.Register();

                Logfile.Warning($"Registered: << \"{$"eq.backpack.{i}.id".LangKey()}\" >>");
            }
            
            CoroutineHost.StartCoroutine(CompatibilityPatchCheck());
        }


        public static bool ShouldPatchCompatibility = false;


        public static IEnumerator CompatibilityPatchCheck()
        {
            yield return PatchingUtils.WaitForChainloader();

            if(!Chainloader.PluginInfos.ContainsKey("com.ramune.RamunesCustomizedStorage"))
                yield break;

            ShouldPatchCompatibility = true;
        }


        public static Dictionary<string, int> IDs = new();
    }
}