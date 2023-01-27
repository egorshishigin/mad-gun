using System;

using Projectiles;

using UnityEngine;

namespace WeaponsShop
{
    [Serializable]
    public class WeaponData
    {
        [SerializeField] private int _id;

        [SerializeField] private GameObject _weaponPrefab;

        [SerializeField] private PlayerProjectile _projectile;

        [SerializeField] private int _price;

        [SerializeField] private bool _bought;

        public int ID => _id;

        public GameObject WeaponPrefab => _weaponPrefab;

        public PlayerProjectile Projectile => _projectile;

        public int Price => _price;

        public bool Bought => _bought;

        public void SetWeaponBoughtState(bool value)
        {
            _bought = value;
        }
    }
}