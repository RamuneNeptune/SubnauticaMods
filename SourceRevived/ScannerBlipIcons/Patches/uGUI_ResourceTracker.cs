

namespace Ramune.ScannerBlipIcons.Patches
{
    [HarmonyPatch(typeof(uGUI_ResourceTracker))]
    public static class uGUI_ResourceTrackerPatch
    {
        [HarmonyPatch(nameof(uGUI_ResourceTracker.GatherNodes)), HarmonyPrefix]
        public static void GatherNodes() => ScannerBlipIcons.RefreshBlacklist();


        [HarmonyPatch(nameof(uGUI_ResourceTracker.UpdateBlips)), HarmonyPostfix]
        public static void UpdateBlips(uGUI_ResourceTracker __instance)
        {
            foreach(var blip in __instance.blips)
            {
                if(blip == null || blip.gameObject == null || !blip.gameObject.activeSelf || blip.techType == TechType.None)
                    continue;

                var icon = GetOrCreateIcon(blip.gameObject);

                icon.enabled = !ScannerBlipIcons.BlacklistedTechTypes.Contains(blip.techType);

                if(!icon.enabled)
                    continue;

                var sprite = SpriteManager.Get(blip.techType);

                if(sprite == null)
                    continue;

                icon.rectTransform.sizeDelta = new Vector2(50f * ScannerBlipIcons.config.BlipIconSizeMultiplier, 50f * ScannerBlipIcons.config.BlipIconSizeMultiplier);
                icon.sprite = sprite;
            }
        }


        public static Image GetOrCreateIcon(GameObject blipObject)
        {
            var existing = blipObject.transform.Find("ScannerBlipIcon");

            if(existing != null && existing.TryGetComponent<Image>(out var image))
                return image;

            var iconObject = new GameObject("ScannerBlipIcon", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            var iconTransform = iconObject.GetComponent<RectTransform>();

            iconTransform.SetParent(blipObject.transform, false);
            iconTransform.anchorMin = new Vector2(0.5f, 0.5f);
            iconTransform.anchorMax = new Vector2(0.5f, 0.5f);
            iconTransform.pivot = new Vector2(0.5f, 0.5f);
            iconTransform.anchoredPosition = Vector2.zero;
            iconTransform.sizeDelta = new Vector2(50f * ScannerBlipIcons.config.BlipIconSizeMultiplier, 50f * ScannerBlipIcons.config.BlipIconSizeMultiplier);

            image = iconObject.GetComponent<Image>();
            image.preserveAspect = true;
            image.raycastTarget = false;

            iconTransform.SetAsLastSibling();

            return image;
        }
    }
}