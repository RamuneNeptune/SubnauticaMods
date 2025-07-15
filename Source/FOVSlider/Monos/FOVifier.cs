

namespace Ramune.FOVSlider.Monos
{
    public class FOVifier : MonoBehaviour
    {
        public float targetFov = 60f;

        public float prevTargetFov = -1f;


        public void Start()
        {
            MiscSettings.fieldOfView = FOVSlider.config.FOV;
        }


        public void Update()
        {
            var camRoot = SNCameraRoot.main;

            targetFov = Player.main.pda.isInUse ? 60f : FOVSlider.config.FOV;

            if(!Mathf.Approximately(prevTargetFov, targetFov) || !Mathf.Approximately(camRoot.CurrentFieldOfView, targetFov))
            {
                prevTargetFov = targetFov;
                camRoot.SetFov(Mathf.Lerp(camRoot.CurrentFieldOfView, targetFov, Time.unscaledDeltaTime * 3f));
            }
        }
    }
}