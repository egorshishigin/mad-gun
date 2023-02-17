using System;
using System.Collections.Generic;

using Zenject;

namespace Weapons
{
    public class Ammo
    {
        private const int _minAmmoGain = 15;

        private const int _maxAmmoGain = 50;

        private List<int> _ammo = new List<int>();

        private AmmoConfig _ammoConfig;

        private WeaponSwitch _weaponSwitch;

        private int _selectedWeapon => _weaponSwitch.SelectedWeapon;

        public List<int> AmmoSupply => _ammo;

        public event Action<int> AmmoCountChanged = delegate { };

        [Inject]
        private Ammo(AmmoConfig ammoConfig, WeaponSwitch weaponSwitch)
        {
            _ammoConfig = ammoConfig;

            _weaponSwitch = weaponSwitch;

            InitializeStartAmmo();
        }

        public void GainAmmo(int amount)
        {
            for (int i = 0; i < _ammo.Count; i++)
            {
                int gainCount = UnityEngine.Random.Range(_minAmmoGain, _maxAmmoGain);

                _ammo[i] += gainCount * amount;
            }

            AmmoCountChanged.Invoke(_ammo[_selectedWeapon]);
        }

        public void SpendAmmo(int id)
        {
            if (_ammo[id] <= 0)
                return;

            _ammo[id]--;

            AmmoCountChanged.Invoke(_ammo[id]);
        }

        private void InitializeStartAmmo()
        {
            foreach (var item in _ammoConfig.StartAmmo)
            {
                _ammo.Add(item);
            }
        }
    }
}