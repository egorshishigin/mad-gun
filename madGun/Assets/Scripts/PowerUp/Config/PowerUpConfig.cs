using System.Collections.Generic;

using UnityEngine;

namespace PowerUp
{
    [CreateAssetMenu(fileName = "PowerUpConfig", menuName = "Configs/PowerUpConfig")]
    public class PowerUpConfig : ScriptableObject
    {
        [SerializeField] private List<PowerUpData> _powerUps;

        public List<PowerUpData> PowerUps => _powerUps;
    }
}