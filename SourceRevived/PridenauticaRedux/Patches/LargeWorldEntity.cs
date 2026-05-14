

namespace Ramune.PridenauticaRedux.Patches
{
    [HarmonyPatch(typeof(LargeWorldEntity))]
    public static class LargeWorldEntityPatch
    {
        [HarmonyPatch(nameof(LargeWorldEntity.Start)), HarmonyPostfix]
        public static void Start(LargeWorldEntity __instance)
        {
            if(!__instance.gameObject.TryGetComponentsInChildren<Renderer>(out var renderers, true))
                return;

            var textures = PridenauticaRedux.config.EnabledTextures
                .Select(x => ImageUtils.GetTexture(x))
                .ToArray();

            if(textures.Length < 1)
                return;

            foreach(var renderer in renderers)
            {
                var selectedTexture = textures[UnityEngine.Random.Range(0, textures.Length)];
                renderer.SetTexture(RamuneLib.Extensions.RendererExtensions.TextureType.Specular, selectedTexture, true);
            }
        }
    }
}