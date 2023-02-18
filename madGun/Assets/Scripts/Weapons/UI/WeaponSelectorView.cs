using Zenject;

using TMPro;

using Data;

using WeaponsShop;

using UnityEngine;
using UnityEngine.UI;

namespace Weapons
{
    public class WeaponSelectorView : MonoBehaviour
    {
        [SerializeField] private WeaponsConfig _config;

        [SerializeField] private Transform _weaponImageHolder;

        [SerializeField] private Transform _weaponNumbersHolder;

        [SerializeField] private TMP_Text _weaponNumber;

        [SerializeField] private HorizontalLayoutGroup _numbersGroup;

        [SerializeField] private int _leftPadding;

        private GameDataIO _gameData;

        private int _weaponCount;

        [Inject]
        private void Construct(GameDataIO gameData)
        {
            _gameData = gameData;
        }

        private void Start()
        {
            for (int i = 0; i < _config.Weapons.Count; i++)
            {
                WeaponData item = _config.Weapons[i];

                if (_gameData.GameData.Weapons[i])
                {
                    var weaponImage = Instantiate(item.WeaponPrefab, _weaponImageHolder.position, item.WeaponPrefab.transform.rotation, _weaponImageHolder);

                    _weaponCount++;

                    var text = Instantiate(_weaponNumber, _weaponNumbersHolder.position, Quaternion.identity, _weaponNumbersHolder);

                    text.text = _weaponCount.ToString();

                    if (_weaponCount == 10)
                    {
                        text.text = "0";
                    }
                    else if (_weaponCount == 11)
                    {
                        text.text = "-";
                    }
                }
            }

            _numbersGroup.padding.left = _leftPadding / _weaponCount;

            if(_weaponCount == 1)
            {
                gameObject.SetActive(false);
            }

            enabled = false;
        }
    }
}