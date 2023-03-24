using System.Collections.Generic;

using UnityEngine;

namespace WeaponsShop
{
    [CreateAssetMenu(fileName = "WeaponsConfig", menuName = "Configs/WeaponsConfig")]
    public class WeaponsConfig : ScriptableObject
    {
        [SerializeField] private List<WeaponData> _weapons = new List<WeaponData>();

        public List<WeaponData> Weapons => _weapons;
    }
}