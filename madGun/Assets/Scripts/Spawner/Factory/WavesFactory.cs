using System.Linq;
using System.Collections.Generic;

using Zenject;

using Timer;

using UnityEngine;

namespace Spawner
{
    public class WavesFactory : IFactory<IWave>
    {
        private readonly DiContainer _container;

        private List<Wave> _wavesPrefabs = new List<Wave>();

        private AnimationCurve _waveLevelFromGameTime;

        private GameTimer _gameTimer;

        [Inject]
        public WavesFactory(DiContainer container, WavesConfig wavesConfig, GameTimer gameTimer)
        {
            _container = container;

            _wavesPrefabs = wavesConfig.Waves;

            _waveLevelFromGameTime = wavesConfig.WaveLevelFromGameTime;

            _gameTimer = gameTimer;
        }

        public IWave Create()
        {
            List<Wave> waves = GetWavesByLevel();

            var wave = GetRandomWave(waves);

            return _container.InstantiatePrefabForComponent<IWave>(wave);
        }

        private List<Wave> GetWavesByLevel()
        {
            int waveLevel = (int)_waveLevelFromGameTime.Evaluate(_gameTimer.GameTime);

            List<Wave> waves = _wavesPrefabs.Where(item => item.Level <= waveLevel).ToList();

            return waves;
        }

        private Wave GetRandomWave(List<Wave> waves)
        {
            int randomIndex = Random.Range(0, waves.Count);

            Wave wave = waves[randomIndex];

            return wave;
        }
    }
}