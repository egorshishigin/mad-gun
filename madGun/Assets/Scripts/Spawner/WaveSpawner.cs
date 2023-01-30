using System.Collections;

using Zenject;

using Timer;

using GamePause;

using UnityEngine;

namespace Spawner
{
    public class WaveSpawner : MonoBehaviour, IInitializable, IPauseHandler
    {
        [SerializeField] private Transform[] _spawnPoints;

        [SerializeField] private Color _spawnPointsGizmos;

        [SerializeField] private AnimationCurve _spawnTimeFromGameTime;

        private WaveFactory _waveFactory;

        private GameTimer _gameTimer;

        private Pause _pause;

        [Inject]
        private void Construct(WaveFactory waveFactory, GameTimer gameTimer, Pause pause)
        {
            _waveFactory = waveFactory;

            _gameTimer = gameTimer;

            _pause = pause;

            _pause.Register(this);
        }

        void IInitializable.Initialize() { }

        private void Start()
        {
            StartSpawn();
        }

        private void OnDestroy()
        {
            _pause.UnRegister(this);
        }

        private void StartSpawn()
        {
            StartCoroutine(SpawnEnemy());
        }

        private IEnumerator SpawnEnemy()
        {
            if (!_pause.Paused)
            {
                Wave wave = (Wave)_waveFactory.Create();

                PlaceWavePrefabAtRandomPoint(wave.transform);
            }

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

        public void SetPause(bool paused)
        {

        }
    }
}