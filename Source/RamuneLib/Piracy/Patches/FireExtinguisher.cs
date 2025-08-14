

namespace RamuneLib.Piracy.Patches
{
    public static class FireExtinguisherPatch
    {
        public static void Update(FireExtinguisher __instance)
        {
            if(!__instance.usedThisFrame || __instance.fuel <= 0f)
                return;

            if(Player.main.IsUnderwater())
            {
                Player.main.GetComponent<UnderwaterMotor>().SetVelocity(-MainCamera.camera.transform.forward * 50f);
            }
            else
            {
                Player.main.groundMotor?.SetVelocity(-MainCamera.camera.transform.forward * 50f);
            }
        }
    }
}