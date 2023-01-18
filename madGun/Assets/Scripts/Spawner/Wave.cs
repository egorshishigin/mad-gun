using UnityEngine;

namespace Spawner
{
    public class Wave : MonoBehaviour, IWave
    {
        [SerializeField] private int _level;

        public int Level => _level;
    }
}