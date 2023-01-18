using System.Collections.Generic;

using UnityEngine;

namespace Spawner
{
    [CreateAssetMenu(fileName = "WavesConfig", menuName = "Configs/WavesConfig")]
    public class WavesConfig : ScriptableObject
    {
        [SerializeField] private List<Wave> _waves = new List<Wave>();

        [SerializeField] private AnimationCurve _waveLevelFromGameTime;

        public List<Wave> Waves => _waves;

        public AnimationCurve WaveLevelFromGameTime => _waveLevelFromGameTime;
    }
}