

namespace Ramune.VehicleBayBeacon.Patches
{
    [HarmonyPatch(typeof(Constructor))]
    public static class ConstructorPatch
    {
        public static PingInstance ping;

        [HarmonyPatch(nameof(Constructor.Deploy)), HarmonyPostfix]
        public static void Start(Constructor __instance, bool value)
        {
            ping = __instance.gameObject.EnsureComponent<PingInstance>();

            ping.SetLabel("signaltext".LangKeyAbbr());

            ping.pingType = PingType.Signal;

            ping.origin = __instance.gameObject.transform;

            if(value)
            {
                ping.visible = true;
                return;
            }

            ping.OnDisable();
        }
    }
}