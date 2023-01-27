using System;
using System.Collections.Generic;

using Zenject;

using WeaponsShop;

using PowerUp;

using UnityEngine;

namespace Data
{
    [Serializable]
    public class GameData
    {
        [SerializeField] private int _coins;

        [SerializeField] private int _highScore;

        [SerializeField] private Dictionary<int, bool> _weaponsInventory = new Dictionary<int, bool>();

        [SerializeField] private Dictionary<int, int> _powerUpsLevels = new Dictionary<int, int>();

        private WeaponsConfig _weaponsConfig;

        private PowerUpConfig _powerUpConfig;

        [Inject]
        public GameData(int coins, int highScore, WeaponsConfig weaponsConfig, PowerUpConfig powerUpConfig)
        {
            _coins = coins;

            _highScore = highScore;

            _weaponsConfig = weaponsConfig;

            _powerUpConfig = powerUpConfig;
        }

        public int Coins => _coins;

        public int HighScore => _highScore;

        public Dictionary<int, bool> Weapons => _weaponsInventory;

        public Dictionary<int, int> PowerUpsLevels => _powerUpsLevels;

        public event Action<int> CoinsSpent = delegate { };

        public void InitializeWeapons()
        {
            _weaponsInventory = new Dictionary<int, bool>();

            foreach (var item in _weaponsConfig.Weapons)
            {
                _weaponsInventory.Add(item.ID, item.Bought);
            }
        }

        public void InitializePowerUps()
        {
            _powerUpsLevels = new Dictionary<int, int>();

            foreach (var item in _powerUpConfig.PowerUps)
            {
                _powerUpsLevels.Add(item.ID, item.Level);
            }
        }

        public void BuyWeapon(int id)
        {
            _weaponsInventory[id] = true;
        }

        public void BuyPowerUp(int id)
        {
            _powerUpsLevels[id]++;
        }

        public void SpendCoins(int amount)
        {
            _coins -= amount;

            CoinsSpent.Invoke(_coins);
        }

        public void GainCoins(int value)
        {
            _coins += value;
        }

        public void SetHighScore(int value)
        {
            _highScore = value;
        }
    }
}