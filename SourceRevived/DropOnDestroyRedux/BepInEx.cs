

namespace Ramune.DropOnDestroyRedux
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class DropOnDestroyRedux : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static DropOnDestroyRedux Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.DropOnDestroyRedux";
        public const string Name = "DropOnDestroyRedux";
        public const string Version = "1.0.0";

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/DropOnDestroyRedux/Version.json"))
                return;
        }
    }


    public static class Extensions
    {
        public static void DropRandom(this Pickupable item, Vector3 position)
        {
            if(item == null)
                return;

            item.Drop(new(position.x + UnityEngine.Random.Range(-3, 3), position.y + UnityEngine.Random.Range(5, 8), position.z + UnityEngine.Random.Range(-3, 3)));
        }


        public static IEnumerator DropCraftingMaterials(Dictionary<TechType, int> craftingMaterialsToDrop, Vector3 position)
        {
            foreach(var craftingMaterial in craftingMaterialsToDrop)
            {
                if(craftingMaterial.Value <= 0)
                    continue;

                var prefabTask = CraftData.GetPrefabForTechTypeAsync(craftingMaterial.Key);

                yield return prefabTask;

                var prefab = prefabTask.GetResult();

                if(prefab == null)
                    continue;

                for(int i = 0; i < craftingMaterial.Value; i++)
                {
                    var gameObject = CraftData.InstantiateFromPrefab(prefab, craftingMaterial.Key);

                    if(gameObject == null)
                        continue;

                    var pickupable = gameObject.GetComponent<Pickupable>();

                    if(pickupable == null)
                        continue;
                    
                    pickupable.DropRandom(position);
                }
            }
        }


        public static int RoundCraftingMaterialAmount(float amount)
        {
            int rounded = DropOnDestroyRedux.config.CraftingMaterialAmountRounding switch
            {
                0 => Mathf.RoundToInt(amount),
                1 => Mathf.FloorToInt(amount),
                2 => Mathf.CeilToInt(amount),
                _ => Mathf.RoundToInt(amount),
            };

            if(DropOnDestroyRedux.config.GuaranteeOneOfEachCraftingMaterial && amount > 0f && rounded == 0)
                return 1;

            return Mathf.Max(0, rounded);
        }
    }
}