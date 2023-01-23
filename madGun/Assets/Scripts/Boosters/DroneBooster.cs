using Zenject;

namespace Boosters
{
    public class DroneBooster : BoosterBase
    {
        private Drone _drone;

        [Inject]
        private void Construct(Drone drone)
        {
            _drone = drone;
        }

        protected override void ActivateBooster()
        {
            _drone.Activate();
        }
    }
}