

namespace RamuneLib.Utils
{
    // rewrite this shit
    public static class VehicleUtils
    {
        public struct SpeedData
        {
            public float Forward;

            public float Backward;

            public float Sideward;

            public float Vertical;


            public SpeedData(float forward, float backward, float sideward, float vertical)
            {
                Forward = forward;
                Backward = backward;
                Sideward = sideward;
                Vertical = vertical;
            }
        }


        public static class Seaglide
        {
            /// <summary>
            /// Retrieves an array of speed values for the Seaglide.
            /// </summary>
            /// <returns>An array containing the Seaglide speed values in the following order: forwardMaxSpeed, backwardMaxSpeed, strafeMaxSpeed, verticalMaxSpeed, waterAcceleration, swimDrag.</returns>
            public static float[] GetSpeeds()
            {
                var controller = Player.main.playerController;

                return new float[]
                {
                    controller.seaglideForwardMaxSpeed,
                    controller.seaglideBackwardMaxSpeed,
                    controller.seaglideStrafeMaxSpeed,
                    controller.seaglideVerticalMaxSpeed,
                    controller.seaglideWaterAcceleration,
                    controller.seaglideSwimDrag
                };
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="multiplier"></param>
            /// <param name="speedDiff"></param>
            /// <param name="accelDiff"></param>
            public static void Speedup(float multiplier, out float speedDiff, out float accelDiff)
            {
                var controller = Player.main.playerController;

                speedDiff = controller.seaglideForwardMaxSpeed * multiplier - controller.seaglideForwardMaxSpeed;
                accelDiff = controller.seaglideWaterAcceleration * multiplier - controller.seaglideWaterAcceleration;

                controller.seaglideForwardMaxSpeed *= multiplier;
                controller.seaglideWaterAcceleration *= multiplier;

                Player.main.UpdateMotorMode();
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="speedDiff"></param>
            /// <param name="accelDiff"></param>
            public static void SpeedDown(float speedDiff, float accelDiff)
            {
                var controller = Player.main.playerController;

                controller.seaglideForwardMaxSpeed -= speedDiff;
                controller.seaglideWaterAcceleration -= accelDiff;

                Player.main.UpdateMotorMode();
            }
        }


        /// <summary>
        /// Retrieves an array of speed values on the provided vehicle component.
        /// </summary>
        /// <returns>An array containing the values on the provided vehicle component in the following order: 0 forwardForce, 1 backwardForce, 2 sidewardForce, 3 verticalForce.</returns>
        public static SpeedData GetSpeeds(this Vehicle vehicle) => new(vehicle.forwardForce, vehicle.backwardForce, vehicle.sidewardForce, vehicle.verticalForce);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="multiplier"></param>
        /// <param name="duration"></param>
        /// <param name="onIncrease"></param>
        /// <param name="onDecrease"></param>
        public static void Speedup(this Vehicle vehicle, float multiplier, float duration = 0, Action onIncrease = null, Action onDecrease = null) => CoroutineHost.StartCoroutine(SpeedupAsync(vehicle, 1, multiplier, duration, onIncrease, onDecrease));
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="multiplier"></param>
        /// <param name="duration"></param>
        /// <param name="onIncrease"></param>
        /// <param name="onDecrease"></param>
        public static void SpeedupForward(this Vehicle vehicle, float multiplier, float duration = 0, Action onIncrease = null, Action onDecrease = null) => CoroutineHost.StartCoroutine(SpeedupAsync(vehicle, 2, multiplier, duration, onIncrease, onDecrease));


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="multiplier"></param>
        /// <param name="duration"></param>
        /// <param name="onIncrease"></param>
        /// <param name="onDecrease"></param>
        public static void SpeedupBackward(this Vehicle vehicle, float multiplier, float duration = 0, Action onIncrease = null, Action onDecrease = null) => CoroutineHost.StartCoroutine(SpeedupAsync(vehicle, 3, multiplier, duration, onIncrease, onDecrease));


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="multiplier"></param>
        /// <param name="duration"></param>
        /// <param name="onIncrease"></param>
        /// <param name="onDecrease"></param>
        public static void SpeedupSideward(this Vehicle vehicle, float multiplier, float duration = 0, Action onIncrease = null, Action onDecrease = null) => CoroutineHost.StartCoroutine(SpeedupAsync(vehicle, 4, multiplier, duration, onIncrease, onDecrease));


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="multiplier"></param>
        /// <param name="duration"></param>
        /// <param name="onIncrease"></param>
        /// <param name="onDecrease"></param>
        public static void SpeedupVertical(this Vehicle vehicle, float multiplier, float duration = 0, Action onIncrease = null, Action onDecrease = null) => CoroutineHost.StartCoroutine(SpeedupAsync(vehicle, 5, multiplier, duration, onIncrease, onDecrease));


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="type"></param>
        /// <param name="multiplier"></param>
        /// <param name="duration"></param>
        /// <param name="onSpeedup"></param>
        /// <param name="onSpeedDown"></param>
        /// <returns></returns>
        private static IEnumerator SpeedupAsync(this Vehicle vehicle, int type, float multiplier, float duration = 0, Action onSpeedup = null, Action onSpeedDown = null)
        {
            var speedData = vehicle.GetSpeeds();

            float forwardDiff = speedData.Forward * multiplier - speedData.Forward;
            float backwardDiff = speedData.Backward * multiplier - speedData.Backward;
            float sidewardDiff = speedData.Sideward * multiplier - speedData.Sideward;
            float verticalDiff = speedData.Vertical * multiplier - speedData.Vertical;

            if(duration <= 0)
                yield break;

            switch(type)
            {
                case 1: 
                    vehicle.forwardForce *= multiplier; 
                    vehicle.backwardForce *= multiplier; 
                    vehicle.sidewardForce *= multiplier; 
                    vehicle.verticalForce *= multiplier; 
                    break;

                case 2: 
                    vehicle.forwardForce *= multiplier;
                    break;

                case 3: 
                    vehicle.backwardForce *= multiplier;
                    break;

                case 4: 
                    vehicle.sidewardForce *= multiplier;
                    break;

                case 5: 
                    vehicle.verticalForce *= multiplier; 
                    break;
            }

            onSpeedup?.Invoke();

            if(duration > 0)
            {
                yield return new WaitForSeconds(duration);
            }

            switch(type)
            {
                case 1: 
                    vehicle.forwardForce -= forwardDiff; 
                    vehicle.backwardForce -= backwardDiff; 
                    vehicle.sidewardForce -= sidewardDiff; 
                    vehicle.verticalForce -= verticalDiff; 
                    break;

                case 2: 
                    vehicle.forwardForce -= forwardDiff; 
                    break;

                case 3: 
                    vehicle.backwardForce -= backwardDiff; 
                    break;

                case 4: 
                    vehicle.sidewardForce -= sidewardDiff; 
                    break;

                case 5: 
                    vehicle.verticalForce -= verticalDiff; 
                    break;
            }

            onSpeedDown?.Invoke();
        }
    }
}