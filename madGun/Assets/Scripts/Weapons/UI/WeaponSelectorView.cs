using UnityEngine;

using WeaponsShop;

using TMPro;

namespace Weapons
{
    public class WeaponSelectorView : MonoBehaviour
    {
        [SerializeField] private WeaponsConfig _config;

        [SerializeField] private Transform _weaponImageHolder;

        [SerializeField] private Transform _weaponNumbersHolder;

        [SerializeField] private TMP_Text _weaponNumber;

        private int _weaponCount;

        private void Start()
        {
            for (int i = 0; i < _config.Weapons.Count; i++)
            {
                WeaponData item = _config.Weapons[i];

                if (item.Bought)
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

            enabled = false;
        }
    }
}