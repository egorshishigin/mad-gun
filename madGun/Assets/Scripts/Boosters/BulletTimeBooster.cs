using Zenject;

namespace Boosters
{
    public class BulletTimeBooster : BoosterBase
    {
        private BulletTime _bulletTime;

        [Inject]
        private void Construct(BulletTime bulletTime)
        {
            _bulletTime = bulletTime;
        }

        protected override void ActivateBooster()
        {
            _bulletTime.Activate();
        }
    }
}