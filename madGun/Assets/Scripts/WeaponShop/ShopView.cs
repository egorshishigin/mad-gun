using System;
using System.Collections.Generic;

using Zenject;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace WeaponsShop
{
    public class ShopView : MonoBehaviour
    {
        [SerializeField] private Button _nextButton;

        [SerializeField] private Button _previousButton;

        [SerializeField] private Button _confirmButton;

        [SerializeField] private Button _buyButton;

        [SerializeField] private Transform _weaponModelHolder;

        [SerializeField] private TMP_Text _price;

        [SerializeField] private Color _normalColor;

        [SerializeField] private Color _disableColor;

        private WeaponsConfig _weaponsConfig;

        private List<GameObject> _weaponsModels = new List<GameObject>();

        public Button BuyButton => _buyButton;

        public event Action NextSelected = delegate { };

        public event Action PreviousSelected = delegate { };

        [Inject]
        private void Construct(WeaponsConfig weaponsConfig)
        {
            _weaponsConfig = weaponsConfig;
        }

        private void OnEnable()
        {
            _nextButton.onClick.AddListener(SelectNext);

            _previousButton.onClick.AddListener(SelectPrevious);
        }

        private void OnDisable()
        {
            _nextButton.onClick.RemoveListener(SelectNext);

            _previousButton.onClick.RemoveListener(SelectPrevious);
        }

        private void Awake()
        {
            InitializeView();
        }

        public void UpdateWeaponModel(int index)
        {
            for (int i = 0; i < _weaponsModels.Count; i++)
            {
                GameObject item = _weaponsModels[i];

                if (i == index)
                {
                    item.SetActive(true);
                }
                else
                {
                    item.SetActive(false);
                }
            }
        }

        public void UpdatePriceText(string value)
        {
            _price.text = value;
        }

        public void SetConfirmButtonState(bool value)
        {
            _confirmButton.image.color = value ? _normalColor : _disableColor;

            _confirmButton.enabled = value;
        }

        public void SetBuyButtonState(bool value)
        {
            _buyButton.gameObject.SetActive(value);
        }

        private void InitializeView()
        {
            foreach (var item in _weaponsConfig.Weapons)
            {
                var weaponModel = Instantiate(item.WeaponPrefab, _weaponModelHolder.position, item.WeaponPrefab.transform.rotation, _weaponModelHolder);

                _weaponsModels.Add(weaponModel);
            }
        }

        private void SelectNext()
        {
            NextSelected.Invoke();
        }

        private void SelectPrevious()
        {
            PreviousSelected.Invoke();
        }
    }
}