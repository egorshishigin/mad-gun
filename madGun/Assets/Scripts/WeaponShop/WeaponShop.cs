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

        private int _itemIndex = 0;

        private GameDataIO _gameDataIO;

        private GameData _gameData;

        private List<WeaponData> _weaponData = new List<WeaponData>();

        public event Action<int> WeaponBought = delegate { };

        [Inject]
        private void Construct(GameDataIO gameDataIO, WeaponsConfig weaponsConfig)
        {
            _weaponsConfig = weaponsConfig;

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
        }

        private void UpdateView()
        {
            _view.UpdateWeaponModel(_itemIndex);

            _view.UpdatePriceText(_weaponsConfig.Weapons[_itemIndex].Price.ToString());

            _view.SetBuyButtonState(!_weaponData[_itemIndex].Bought);
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
            if (_gameData.Coins < _weaponData[_itemIndex].Price)
                return;

            _gameData.BuyWeapon(_itemIndex);

            _gameData.SpendCoins(_weaponData[_itemIndex].Price);

            UpdateWeaponData();

            _gameDataIO.SaveGameData();

            _view.SetBuyButtonState(!_weaponData[_itemIndex].Bought);
        }

        private void SelectNextWeapon()
        {
            if (_itemIndex < _weaponsConfig.Weapons.Count - 1)
            {
                _itemIndex++;

                UpdateView();
            }
        }

        private void SelectPreviousWeapon()
        {
            if (_itemIndex > 0)
            {
                _itemIndex--;

                UpdateView();
            }
        }
    }
}