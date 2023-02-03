using System.Collections.Generic;

using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "AmmoConfig", menuName = "Configs/AmmoConfig")]
    public class AmmoConfig : ScriptableObject
    {
        [SerializeField] private List<int> _startAmmo = new List<int>();

        public List<int> StartAmmo => _startAmmo;
    }
}