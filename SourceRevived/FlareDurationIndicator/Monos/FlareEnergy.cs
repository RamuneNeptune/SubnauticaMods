

namespace Ramune.FlareDurationIndicator.Monos
{
    public class FlareEnergy : MonoBehaviour
    {
        public EnergyMixin energyMixin;

        public Flare flare;

        public float maxEnergy = 1800f;


        public void Awake()
        {
            flare = GetComponent<Flare>();
        }


        public void Update()
        {
            if(energyMixin == null)
                energyMixin = GetComponent<EnergyMixin>();

            if(energyMixin == null || energyMixin.battery == null || flare == null || !flare.flareActiveState)
                return;

            energyMixin.battery.charge = Mathf.Max(0f, Mathf.Min(100f, flare.energyLeft / maxEnergy * 100f));
        }
    }
}