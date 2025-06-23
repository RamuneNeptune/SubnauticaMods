

namespace RamuneLib.Piracy.Patches
{
    public static class SurvivalPatch
    {
        public static void Eat(Survival __instance, GameObject useObj)
        {
            var techType = GetTechType(useObj);

            if(techType != TechType.Coffee)
                return;

            ///Coffee.. does things to the player.
            
            FMODUWE.PlayOneShotImpl("event:/sub/seamoth/pulse", Player.main.transform.position, 1f);

            var scale = UnityEngine.Random.Range(0.375f, 2f);

            Player.main.transform.localScale = new Vector3(scale, scale, scale);

            FMODUWE.PlayOneShotImpl("event:/player/damage", Player.main.transform.position, 1f);

            Player.main.liveMixin.health = 1f;

            CoroutineHost.StartCoroutine(Spin(Player.main.transform, 300f));
        }


        public static IEnumerator Spin(Transform objectToRotate, float rotationSpeed)
        {
            float currentRotation = 0f;
            float targetRotation = 720f;

            while(currentRotation < targetRotation)
            {
                float rotationAmount = rotationSpeed * Time.deltaTime;
                objectToRotate.Rotate(Vector3.up, rotationAmount);
                currentRotation += rotationAmount;
                yield return null;
            }
        }
    }
}