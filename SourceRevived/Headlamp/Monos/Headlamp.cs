

namespace Ramune.Headlamp.Monos
{
    public class Headlamp : MonoBehaviour
    {
        public (FMODAsset On, FMODAsset Off) lightSounds = (Utility.AudioUtils.GetFmodAsset("event:/sub/seamoth/seaglide_light_on"), Utility.AudioUtils.GetFmodAsset("event:/sub/seamoth/seaglide_light_off"));

        public GameObject lightRoot;

        public Light light;

        public Color currentColor = new(Ramune.Headlamp.Headlamp.config.Red, Ramune.Headlamp.Headlamp.config.Green, Ramune.Headlamp.Headlamp.config.Blue);

        public bool lightActive, doRainbow;

        public float offset = 0.94f;

        public float currentTime;

        public int lightState;

        public static Headlamp main;


        public void Start()
        {
            main = this;

            lightRoot = new GameObject("HeadlampRoot");
            lightRoot.transform.parent = Player.main.transform;
            light = lightRoot.gameObject.AddComponent<Light>();
            light.enabled = true;
            light.color = currentColor;
            light.range = 30f;
            light.intensity = 0.9f;
            light.spotAngle = 90f;
            light.innerSpotAngle = 80f;
            light.type = LightType.Spot;
            light.shape = LightShape.Cone;
            light.shadows = LightShadows.Hard;
        }


        public void SetLightState(bool state)
        {
            light.enabled = state;
            lightActive = state;
        }
        


        public void Update()
        {
            lightRoot.transform.position = Inventory.main.cameraSocket.position + Inventory.main.cameraSocket.forward * offset;
            lightRoot.transform.rotation = MainCamera.camera.transform.rotation;
            lightRoot.transform.eulerAngles = MainCamera.camera.transform.eulerAngles;

            if(Player.main.isPiloting)
            {
                light.enabled = false;
                return;
            }

            if(!Cursor.visible && GameInput.GetButtonDown(Ramune.Headlamp.Headlamp.ToggleHeadlamp))
            {
                FMODUWE.PlayOneShot(lightActive ? lightSounds.Off : lightSounds.On, transform.position, 1f);

                SetLightState(!lightActive);
            }

            if(!doRainbow)
                return;
                
            Rainbow();
        }


        public void Refresh()
        {
            currentColor.r = Ramune.Headlamp.Headlamp.config.Red;
            currentColor.g = Ramune.Headlamp.Headlamp.config.Green;
            currentColor.b = Ramune.Headlamp.Headlamp.config.Blue;
            light.range = 30f * Ramune.Headlamp.Headlamp.config.Range;
            light.intensity = 0.9f * Ramune.Headlamp.Headlamp.config.Intensity;
            light.spotAngle = 90f * Ramune.Headlamp.Headlamp.config.Conesize;
            light.innerSpotAngle = 80f * Ramune.Headlamp.Headlamp.config.Conesize;
            light.color = currentColor;
        }


        public void Rainbow()
        {
            currentTime += Time.deltaTime / Ramune.Headlamp.Headlamp.config.Red;

            if(currentTime >= 1f)
                currentTime -= 1f;

            light.color = Color.HSVToRGB(currentTime, Ramune.Headlamp.Headlamp.config.Red, Ramune.Headlamp.Headlamp.config.Red);
        }
    }
}