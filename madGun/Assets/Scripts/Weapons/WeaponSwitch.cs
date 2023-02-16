using Zenject;

using Data;

using UnityEngine;
using System.Collections.Generic;

namespace Weapons
{
    public class WeaponSwitch : MonoBehaviour
    {
        [SerializeField] private List<Weapon> _weapons = new List<Weapon>();

        [SerializeField] private AmmoView _view;

        private GameDataIO _gameData;

        private int _weaponIndex = 0;

        private int _selectedWeapon;

        public int SelectedWeapon => _selectedWeapon;

        public List<Weapon> Weapons => _weapons;

        [Inject]
        private void Construct(GameDataIO gameData)
        {
            _gameData = gameData;
        }

        private void Start()
        {
            for (int i = _weapons.Count - 1; i >= 0; i--)
            {
                if (_gameData.GameData.Weapons[i] == false)
                {
                    Destroy(_weapons[i].gameObject);

                    _weapons.Remove(_weapons[i]);
                }
            }

            EnableWeapon(0);

            UpdateView(0);
        }

        private void Update()
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                PreviousWeapon();
            }

            if (Input.mouseScrollDelta.y < 0)
            {
                NextWeapon();
            }

            KeyBoardNumbersWeaponSwitch();
        }

        private void KeyBoardNumbersWeaponSwitch()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _weaponIndex = 0;

                if (_weaponIndex >= _weapons.Count)
                    return;

                EnableWeapon(_weaponIndex);

                UpdateView(_weapons[_weaponIndex].ID);

                _selectedWeapon = _weapons[_weaponIndex].ID;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _weaponIndex = 1;

                if (_weaponIndex >= _weapons.Count)
                    return;

                EnableWeapon(_weaponIndex);

                UpdateView(_weapons[_weaponIndex].ID);

                _selectedWeapon = _weapons[_weaponIndex].ID;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _weaponIndex = 2;

                if (_weaponIndex >= _weapons.Count)
                    return;

                EnableWeapon(_weaponIndex);

                UpdateView(_weapons[_weaponIndex].ID);

                _selectedWeapon = _weapons[_weaponIndex].ID;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                _weaponIndex = 3;

                if (_weaponIndex >= _weapons.Count)
                    return;

                EnableWeapon(_weaponIndex);

                UpdateView(_weapons[_weaponIndex].ID);

                _selectedWeapon = _weapons[_weaponIndex].ID;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                _weaponIndex = 4;

                if (_weaponIndex >= _weapons.Count)
                    return;

                EnableWeapon(_weaponIndex);

                UpdateView(_weapons[_weaponIndex].ID);

                _selectedWeapon = _weapons[_weaponIndex].ID;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                _weaponIndex = 5;

                if (_weaponIndex >= _weapons.Count)
                    return;

                EnableWeapon(_weaponIndex);

                UpdateView(_weapons[_weaponIndex].ID);

                _selectedWeapon = _weapons[_weaponIndex].ID;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                _weaponIndex = 6;

                if (_weaponIndex >= _weapons.Count)
                    return;

                EnableWeapon(_weaponIndex);

                UpdateView(_weapons[_weaponIndex].ID);

                _selectedWeapon = _weapons[_weaponIndex].ID;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                _weaponIndex = 7;

                if (_weaponIndex >= _weapons.Count)
                    return;

                EnableWeapon(_weaponIndex);

                UpdateView(_weapons[_weaponIndex].ID);

                _selectedWeapon = _weapons[_weaponIndex].ID;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                _weaponIndex = 8;

                if (_weaponIndex >= _weapons.Count)
                    return;

                EnableWeapon(_weaponIndex);

                UpdateView(_weapons[_weaponIndex].ID);

                _selectedWeapon = _weapons[_weaponIndex].ID;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                _weaponIndex = 9;

                if (_weaponIndex >= _weapons.Count)
                    return;

                EnableWeapon(_weaponIndex);

                UpdateView(_weapons[_weaponIndex].ID);

                _selectedWeapon = _weapons[_weaponIndex].ID;
            }
            else if (Input.GetKeyDown(KeyCode.Minus))
            {
                _weaponIndex = 10;

                if (_weaponIndex >= _weapons.Count)
                    return;

                EnableWeapon(_weaponIndex);

                UpdateView(_weapons[_weaponIndex].ID);

                _selectedWeapon = _weapons[_weaponIndex].ID;
            }
        }

        private void NextWeapon()
        {
            if (_weaponIndex < _weapons.Count - 1)
            {
                _weaponIndex++;

                EnableWeapon(_weaponIndex);

                UpdateView(_weapons[_weaponIndex].ID);

                _selectedWeapon = _weapons[_weaponIndex].ID;
            }
        }

        private void PreviousWeapon()
        {
            if (_weaponIndex > 0)
            {
                _weaponIndex--;

                EnableWeapon(_weaponIndex);

                UpdateView(_weapons[_weaponIndex].ID);

                _selectedWeapon = _weapons[_weaponIndex].ID;
            }
        }

        private void EnableWeapon(int id)
        {
            for (int i = 0; i < _weapons.Count; i++)
            {
                GameObject item = _weapons[i].gameObject;

                if (i == id)
                {
                    item.SetActive(true);
                }
                else
                {
                    item.SetActive(false);
                }
            }
        }

        private void UpdateView(int id)
        {
            _view.UpdateAmmoByID(id);
        }
    }
}