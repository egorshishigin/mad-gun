using Zenject;

namespace Boosters
{
    public class DroneBooster : BoosterBase
    {
        private FireRain _drone;

        [Inject]
        private void Construct(FireRain drone)
        {
            _drone = drone;
        }

        protected override void ActivateBooster()
        {
            _drone.Activate();
        }
    }
}