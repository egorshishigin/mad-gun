using Zenject;

using Weapons;

namespace Boosters
{
    public class DoubleGun : ActiveBoosterBase
    {
        private WeaponSwitcher _weaponSwitch;

        [Inject]
        private void Construct(WeaponSwitcher weaponSwitch)
        {
            _weaponSwitch = weaponSwitch;
        }

        protected override void OnActivated()
        {
            foreach (var item in _weaponSwitch.Weapons)
            {
                item.SetDoubleGunState(true);
            }
        }

        protected override void OnDectivated()
        {
            foreach (var item in _weaponSwitch.Weapons)
            {
                item.SetDoubleGunState(false);
            }
        }
    }
}