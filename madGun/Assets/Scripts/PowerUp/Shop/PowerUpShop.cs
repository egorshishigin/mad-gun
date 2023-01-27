using System.Collections.Generic;

using Zenject;

using Data;

using UnityEngine;

namespace PowerUp
{
    public class PowerUpShop : MonoBehaviour
    {
        [SerializeField] private PowerUpShopView _view;

        private PowerUpConfig _powerUpConfig;

        private GameDataIO _gameDataIO;

        private GameData _gameData;

        private List<PowerUpData> _powerUps = new List<PowerUpData>();

        [Inject]
        private void Construct(PowerUpConfig powerUpConfig, GameDataIO gameDataIO)
        {
            _powerUpConfig = powerUpConfig;

            _gameDataIO = gameDataIO;

            _gameData = gameDataIO.GameData;

            _powerUps = _powerUpConfig.PowerUps;
        }

        private void OnDisable()
        {
            foreach (var item in _view.PowerUpItemViews)
            {
                item.PowerUpBought -= BuyPowerUp;
            }
        }

        private void Start()
        {
            foreach (var item in _view.PowerUpItemViews)
            {
                item.PowerUpBought += BuyPowerUp;
            }

            UpdatePowerUpsData();

            foreach (var item in _powerUps)
            {
                _view.PowerUpItemViews[item.ID].UpdatePowerUpLevel(_gameData.PowerUpsLevels[item.ID]);
            }
        }

        private void BuyPowerUp(int id)
        {
            if (_gameData.Coins < _powerUps[id].Price)
                return;

            _gameData.BuyPowerUp(id);

            _gameData.SpendCoins(_powerUps[id].Price);

            UpdatePowerUpsData();

            _gameDataIO.SaveGameData();

            UpdateView(id, _gameData.PowerUpsLevels[id], _powerUps[id].Price);
        }

        private void UpdateView(int id, int level, int price)
        {
            _view.PowerUpItemViews[id].UpdatePowerUpLevel(level);

            _view.PowerUpItemViews[id].UpdatePriceText(price);
        }

        private void UpdatePowerUpsData()
        {
            foreach (var item in _powerUps)
            {
                item.SetLevel(_gameData.PowerUpsLevels[item.ID]);
            }
        }
    }
}