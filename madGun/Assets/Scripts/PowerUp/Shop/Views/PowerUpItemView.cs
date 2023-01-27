using System;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace PowerUp
{
    public class PowerUpItemView : MonoBehaviour
    {
        private const int _maxLevel = 5;

        [SerializeField] private Button _butButton;

        [SerializeField] private Image _icon;

        [SerializeField] private TMP_Text _priceText;

        [SerializeField] private TMP_Text _descriptionText;

        [SerializeField] private Image _powerUpLevel;

        private int _id;

        public event Action<int> PowerUpBought = delegate { };

        private void OnDisable()
        {
            _butButton.onClick.RemoveListener(BuyPowerUp);
        }

        public void Initialize(PowerUpData powerUpData)
        {
            _id = powerUpData.ID;

            _icon.sprite = powerUpData.Icon;

            _priceText.text = powerUpData.Price.ToString();

            _descriptionText.text = powerUpData.Description;

            _butButton.onClick.AddListener(BuyPowerUp);
        }

        public void UpdatePowerUpLevel(int level)
        {
            _powerUpLevel.fillAmount = 0.2f * level;

            if(level >= _maxLevel)
            {
                DisableBuyButton();
            }
        }

        public void UpdatePriceText(int price)
        {
            _priceText.text = price.ToString();
        }

        private void DisableBuyButton()
        {
            _butButton.gameObject.SetActive(false);
        }

        private void BuyPowerUp()
        {
            PowerUpBought.Invoke(_id);
        }
    }
}