using System;
using System.Collections.Generic;

using Zenject;

using Data;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons
{
    public class WeaponSwitcher : MonoBehaviour
    {
        [SerializeField] private List<Weapon> _weapons = new List<Weapon>();

        [SerializeField] private AmmoView _view;

        [SerializeField] private InputActionReference _nextAction;

        [SerializeField] private InputActionReference _previousAction;

        private GameDataIO _gameData;

        private int _weaponIndex = 0;

        private int _selectedWeapon;

        public int SelectedWeapon => _selectedWeapon;

        public List<Weapon> Weapons => _weapons;

        public Weapon CurrentWeapon => _weapons[_weaponIndex];

        public event Action WeaponSwitched = delegate { };

        [Inject]
        private void Construct(GameDataIO gameData)
        {
            _gameData = gameData;
        }

        private void OnEnable()
        {
            _nextAction.action.Enable();

            _nextAction.action.performed += _ => NextWeapon();

            _previousAction.action.Enable();

            _previousAction.action.performed += _ => PreviousWeapon();
        }

        private void OnDisable()
        {
            _nextAction.action.Disable();

            _nextAction.action.performed -= _ => NextWeapon();

            _previousAction.action.Disable();

            _previousAction.action.performed -= _ => PreviousWeapon();
        }

        private void Start()
        {
            if (_gameData.GameData == null)
            {
                Debug.LogWarning("Game data has not been loaded. All weapons available. Launch game from Boot scene to load saved data");

                return;
            }
            else
            {
                for (int i = _weapons.Count - 1; i >= 0; i--)
                {
                    if (_gameData.GameData.Weapons[i] == false)
                    {
                        Destroy(_weapons[i].gameObject);

                        _weapons.Remove(_weapons[i]);
                    }
                }
            }


            EnableWeapon(0);

            UpdateView(0);
        }

        private void NextWeapon()
        {
            if (_weaponIndex < _weapons.Count - 1)
            {
                _weaponIndex++;

                EnableWeapon(_weaponIndex);

                UpdateView(_weapons[_weaponIndex].ID);

                _selectedWeapon = _weapons[_weaponIndex].ID;

                WeaponSwitched.Invoke();
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

                WeaponSwitched.Invoke();
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