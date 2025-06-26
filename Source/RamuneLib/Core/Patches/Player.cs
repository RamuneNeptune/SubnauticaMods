

namespace RamuneLib.Patches
{
    public static class PlayerPatch
    {
        public static Initializer.ModData modData;

        public static void Start()
        {
            Screen.Warning($"<color=#ffac00>{Variables.name}</color> has an update available ({modData.Latest}). See your 'LogOutput.log' file for more information");
        }
    }
}