

namespace Ramune.FOVSlider.Monos
{
    public class FOVifier : MonoBehaviour
    {
        public void Start()
        {
            MiscSettings.fieldOfView = FOVSlider.config.FOV;
        }


        public void FixedUpdate()
        {
            if(Player.main.pda.isInUse || Player.main.camRoot.mainCamera.fieldOfView == FOVSlider.config.FOV)
                return;
            
            Player.main.camRoot.SyncFieldOfView();
        }
    }
}