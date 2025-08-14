

namespace RamuneLib.Piracy.Patches
{
    public static class StasisSpherePatch
    {
        public static void Shoot(StasisSphere __instance)
        {
            __instance.radius = 70f;
        }

        public static float time = 0f;

        public static bool UpdateMaterials(StasisSphere __instance)
        {
            time += Time.deltaTime * 0.2f;
            time %= 1f;

            var rainbow = Color.HSVToRGB(time, 1f, 1f);

            __instance.GetComponent<Renderer>().materials[0].SetColor(ShaderPropertyID._Color, rainbow);
            __instance.GetComponent<Renderer>().materials[1].SetColor(ShaderPropertyID._Color, rainbow);

            return false;
        }
    }
}