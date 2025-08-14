

namespace RamuneLib.Piracy.Patches
{
    public static class ExosuitPatch
    {
        public static void ApplyJumpForce(Exosuit __instance)
        {
            if(!(__instance.timeLastJumped + 1f <= Time.time))
                return;
            
            __instance.useRigidbody.AddForce(Vector3.up * 30f, ForceMode.VelocityChange);
        }
    }
}