using System;

using UnityEngine;

namespace PowerUp
{
    [Serializable]
    public class PowerUpData
    {
        private const int _maxLevel = 5;

        [SerializeField] private int _id;

        [SerializeField] private Sprite _icon;

        [SerializeField] private int _price;

        [SerializeField] private int _priceGrowAmount;

        [SerializeField] private string _description;

        [SerializeField] private int _level;

        public int ID => _id;

        public Sprite Icon => _icon;

        public int Price => _price + (_level * _priceGrowAmount);

        public string Description => _description;

        public int Level => _level;

        public void SetLevel(int level)
        {
            _level = level;
        }

        public void LevelUp()
        {
            if (_level < _maxLevel)
            {
                _level++;
            }
            else return;
        }
    }
}