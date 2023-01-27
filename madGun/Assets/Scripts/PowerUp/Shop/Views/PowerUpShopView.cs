using Zenject;

using UnityEngine;
using System.Collections.Generic;

namespace PowerUp
{
    public class PowerUpShopView : MonoBehaviour
    {
        [SerializeField] private Transform _itemsHolder;

        [SerializeField] private PowerUpItemView _itemViewPrefab;

        private PowerUpConfig _powerUpConfig;

        private List<PowerUpItemView> _powerUpItemViews = new List<PowerUpItemView>();

        public List<PowerUpItemView> PowerUpItemViews => _powerUpItemViews;

        [Inject]
        private void Construct(PowerUpConfig powerUpConfig)
        {
            _powerUpConfig = powerUpConfig;
        }

        private void Awake()
        {
            foreach (var item in _powerUpConfig.PowerUps)
            {
                var view = Instantiate(_itemViewPrefab, _itemsHolder);

                view.Initialize(item);

                _powerUpItemViews.Add(view);
            }
        }
    }
}