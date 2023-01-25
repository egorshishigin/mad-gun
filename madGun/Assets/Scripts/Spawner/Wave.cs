using Zenject;

using HealthSystem;

using Score;

using UnityEngine;

namespace Spawner
{
    public class Wave : MonoBehaviour, IWave
    {
        [SerializeField] private int _level;

        [SerializeField] private int _killScore;

        [SerializeField] private Health[] _enemies;

        private GameScore _gameScore;

        private int _enemyCount;

        private int _destroyedEnemies;

        public int Level => _level;

        [Inject]
        private void Construct(GameScore gameScore)
        {
            _gameScore = gameScore;
        }

        public void WaveScore()
        {
            _gameScore.IncreaseScore(_killScore);
        }

        private void OnEnable()
        {
            _enemyCount = _enemies.Length;

            for (int i = 0; i < _enemies.Length; i++)
            {
                _enemies[i].Died += WaveScore;

                _enemies[i].Destroyed += EnemyDestroyedHandler;
            }
        }

        private void EnemyDestroyedHandler()
        {
            _destroyedEnemies++;

            if (_destroyedEnemies == _enemyCount)
            {
                Destroy(gameObject, 5f);
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < _enemies.Length; i++)
            {
                _enemies[i].Died -= WaveScore;
            }
        }
    }
}