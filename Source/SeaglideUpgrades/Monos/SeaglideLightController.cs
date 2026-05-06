

namespace Ramune.SeaglideUpgrades.Monos
{
    public static class SeaglideLightControllerManager
    {
        public static readonly Dictionary<TechType, HashSet<SeaglideLightController>> seaglideLightControllers = [];

        public static readonly List<Action<SeaglideLightController, SeaglideLightSettings>> ModifyLightsCallbacks = [];

        public sealed class SeaglideLightSettings
        {
            public Color color;
            public float range;
            public float intensity;
            public float conesize;
        }

        public static void Add(SeaglideLightController lightController)
        {
            if(!seaglideLightControllers.TryGetValue(lightController.techType, out var set))
                seaglideLightControllers[lightController.techType] = set = [];

            set.Add(lightController);
        }

        public static void Remove(SeaglideLightController lightController, TechType techType)
        {
            if(!seaglideLightControllers.TryGetValue(techType, out var lightControllers)) 
                return;

            lightControllers.Remove(lightController);

            if(lightControllers.Count == 0)
                seaglideLightControllers.Remove(techType);
        }

        public static void Apply(TechType techType, Color color, float range, float intensity, float conesize)
        {
            if(!seaglideLightControllers.TryGetValue(techType, out var lightControllers))
                return;

            foreach(var lightController in lightControllers.ToArray())
            {
                if(lightController == null)
                    continue;

                var settings = new SeaglideLightSettings
                {
                    color = color,
                    range = range,
                    intensity = intensity,
                    conesize = conesize
                };

                foreach(var modifier in ModifyLightsCallbacks.ToArray())
                {
                    try
                    {
                        modifier?.Invoke(lightController, settings);
                    }
                    catch(Exception ex)
                    {
                        Logfile.Error($"Failed to invoke Seaglide light modifier:\n{ex}");
                    }
                }

                lightController.Apply(settings.color, settings.range, settings.intensity, settings.conesize);
            }
        }
    }


    public class SeaglideLightController : MonoBehaviour
    {
        public Seaglide seaglide;

        public Light[] lights;

        public TechType techType;

        public float defaultRange = 200f;

        public float defaultIntensity = 0.9f;

        public float defaultConeSize = 70f;


        public void Start()
        {
            seaglide = GetComponent<Seaglide>();

            if(seaglide == null || seaglide.toggleLights == null || !seaglide.toggleLights.lightsParent.TryGetComponentsInChildren(out lights, true)) 
                return;

            SeaglideLightControllerManager.Add(this);

            foreach(var light in lights)
            {
                if(light == null) 
                    continue;

                light.gameObject.SetActive(true);
            }

            switch(techType)
            {
                case var x when x == Items.SeaglideMK1.Prefab.Info.TechType:
                    Config.OnChangeMK1();
                    break;

                case var x when x == Items.SeaglideMK2.Prefab.Info.TechType:
                    Config.OnChangeMK2();
                    break;

                case var x when x == Items.SeaglideMK3.Prefab.Info.TechType:
                    Config.OnChangeMK3();
                    break;
            }
        }


        public void OnDestroy() => SeaglideLightControllerManager.Remove(this, techType);


        public void Apply(Color color, float range, float intensity, float conesize)
        {
            if(lights == null || lights.Length == 0) 
                return;

            foreach(var light in lights)
            {
                if(light == null) 
                    continue;

                light.color = color;
                light.range = defaultRange * range;
                light.intensity = defaultIntensity * intensity;
                light.spotAngle = defaultConeSize * conesize;
            }
        }
    }
}