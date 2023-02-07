using Zenject;

using Weapons;

using UnityEngine;

namespace Boosters
{
    public class AmmoPickUp : BoosterBase, IUpgradetable
    {
        [SerializeField] private int _ammoPickUp;

        private Ammo _ammo;

        [Inject]
        private void Construct(Ammo ammo)
        {
            _ammo = ammo;
        }

        protected override void ActivateBooster()
        {
            _ammo.GainAmmo(_ammoPickUp);
        }

        public void Upgrade(int timeAmount, int countAmount)
        {
            _ammoPickUp += countAmount;
        }
    }
}