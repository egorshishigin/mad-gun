using Zenject;

namespace Boosters
{
    public class FireRainBooster : BoosterBase
    {
        private FireRain _fireRain;

        [Inject]
        private void Construct(FireRain fireRain)
        {
            _fireRain = fireRain;
        }

        protected override void ActivateBooster()
        {
            _fireRain.Activate();
        }
    }
}