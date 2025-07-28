

namespace RamuneLib.Piracy.Patches
{
    public static class SurvivalPatch
    {
        public static void Eat()
        {
            FMODUWE.PlayOneShotImpl("event:/sub/seamoth/pulse", Player.main.transform.position, 1f);

            FMODUWE.PlayOneShotImpl("event:/player/damage", Player.main.transform.position, 1f);

            MainCameraControl.main.ShakeCamera(10f, 4f, MainCameraControl.ShakeMode.Linear, 1.4f);

            CoroutineHost.StartCoroutine(Spin(Player.main.transform, 300f));
        }


        public static IEnumerator Spin(Transform ᠢ, float Ӏ)
        {
            float І = 0f, Ι = 0x2D0;

            while(І < Ι)
            {
                var I = Ӏ * Time.deltaTime;
                ᠢ.Rotate(Vector3.up, I);
                І += I;
                yield return null;
            }
        }
    }
}