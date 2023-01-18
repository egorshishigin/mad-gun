using System.Collections;

using Zenject;

using Timer;

using UnityEngine;

namespace Spawner
{
    public class WaveSpawner : MonoBehaviour, IInitializable
    {
        [SerializeField] private Transform[] _spawnPoints;

        [SerializeField] private Color _spawnPointsGizmos;

        [SerializeField] private AnimationCurve _spawnTimeFromGameTime;

        private WaveFactory _waveFactory;

        private GameTimer _gameTimer;

        [Inject]
        private void Construct(WaveFactory waveFactory, GameTimer gameTimer)
        {
            _waveFactory = waveFactory;

            _gameTimer = gameTimer;
        }

        public void Initialize()
        {

        }

        private void Start()
        {
            StartSpawn();
        }

        private void StartSpawn()
        {
            StartCoroutine(SpawnEnemy());
        }

        private IEnumerator SpawnEnemy()
        {
            Wave wave = (Wave)_waveFactory.Create();

            PlaceWavePrefabAtRandomPoint(wave.transform);

            yield return new WaitForSeconds(_spawnTimeFromGameTime.Evaluate(_gameTimer.GameTime));

            StartSpawn();
        }

        private void PlaceWavePrefabAtRandomPoint(Transform prefab)
        {
            int randomIndex = Random.Range(0, _spawnPoints.Length);

            prefab.position = _spawnPoints[randomIndex].position;
        }

        private void OnDrawGizmos()
        {
            if (_spawnPoints == null)
                return;

            Gizmos.color = _spawnPointsGizmos;

            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                Gizmos.DrawCube(_spawnPoints[i].position, Vector3.one);
            }
        }
    }
}