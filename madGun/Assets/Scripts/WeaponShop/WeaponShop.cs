using System;
using System.Collections.Generic;

using Zenject;

using Projectiles;

using Data;

using UnityEngine;

namespace WeaponsShop
{
    public class WeaponShop : MonoBehaviour
    {
        [SerializeField] private ShopView _view;

        private WeaponsConfig _weaponsConfig;

        private int _itemIdex = 0;

        private WeaponSettings _weaponSettings;

        private GameDataIO _gameDataIO;

        private GameData _gameData;

        private List<WeaponData> _weaponData = new List<WeaponData>();

        public event Action<int> WeaponBought = delegate { };

        [Inject]
        private void Construct(WeaponSettings weaponSettings, GameDataIO gameDataIO, WeaponsConfig weaponsConfig)
        {
            _weaponsConfig = weaponsConfig;

            _weaponSettings = weaponSettings;

            _gameDataIO = gameDataIO;

            _gameData = _gameDataIO.GameData;

            _weaponData = _weaponsConfig.Weapons;
        }

        private void OnEnable()
        {
            _view.NextSelected += SelectNextWeapon;

            _view.PreviousSelected += SelectPreviousWeapon;

            _view.BuyButton.onClick.AddListener(BuyWeapon);

            UpdateWeaponData();
        }

        private void OnDisable()
        {
            _view.NextSelected -= SelectNextWeapon;

            _view.PreviousSelected -= SelectPreviousWeapon;

            _view.BuyButton.onClick.RemoveListener(BuyWeapon);
        }

        private void Start()
        {
            UpdateView();

            SaveSelectedWeapon(_weaponsConfig.Weapons[_itemIdex].Projectile, _itemIdex);
        }

        private void UpdateView()
        {
            _view.UpdateWeaponModel(_itemIdex);

            _view.UpdatePriceText(_weaponsConfig.Weapons[_itemIdex].Price.ToString());

            _view.SetConfirmButtonState(_weaponData[_itemIdex].Bought);

            _view.SetBuyButtonState(!_weaponData[_itemIdex].Bought);
        }

        private void UpdateWeaponData()
        {
            foreach (var item in _weaponData)
            {
                item.SetWeaponBoughtState(_gameData.Weapons[item.ID]);
            }
        }

        private void BuyWeapon()
        {
            if (_gameData.Coins < _weaponData[_itemIdex].Price)
                return;

            _gameData.BuyWeapon(_itemIdex);

            _gameData.SpendCoins(_weaponData[_itemIdex].Price);

            UpdateWeaponData();

            _gameDataIO.SaveGameData();

            _view.SetBuyButtonState(!_weaponData[_itemIdex].Bought);

            _view.SetConfirmButtonState(_weaponData[_itemIdex].Bought);
        }

        private void SelectNextWeapon()
        {
            if (_itemIdex < _weaponsConfig.Weapons.Count - 1)
            {
                _itemIdex++;

                UpdateView();

                SaveSelectedWeapon(_weaponsConfig.Weapons[_itemIdex].Projectile, _itemIdex);
            }
        }

        private void SelectPreviousWeapon()
        {
            if (_itemIdex > 0)
            {
                _itemIdex--;

                UpdateView();

                SaveSelectedWeapon(_weaponsConfig.Weapons[_itemIdex].Projectile, _itemIdex);
            }
        }

        private void SaveSelectedWeapon(PlayerProjectile projectile, int id)
        {
            _weaponSettings.SetWeaponPrefab(projectile);

            PlayerPrefs.SetInt("Weapon", id);

            PlayerPrefs.Save();
        }
    }
}