using System.Collections;

using Zenject;

using Player;

using HealthSystem;

using GamePause;

using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class ShootingEnemy : MonoBehaviour, IEnemy, IPauseHandler
    {
        [SerializeField] private NavMeshAgent _meshAgent;

        [SerializeField] private Health _health;

        [SerializeField] private int _damage;

        [SerializeField] private float _destroyTime;

        [SerializeField] private GameObject _rootObject;

        [SerializeField] private EnemyShooting _shooting;

        [SerializeField] private float _fireRate;

        [SerializeField] private float _shootingTime;

        [SerializeField] private ShootingEnemyAnimation _enemyAnimation;

        private PlayerHitBox _player;

        private float _nextTimeToShoot;

        private bool _shoot = false;

        private float _currentShootTime;

        private Pause _pause;

        [Inject]
        private void Construct(PlayerHitBox player, Pause pause)
        {
            _player = player;

            _pause = pause;

            _pause.Register(this);
        }

        private void OnEnable()
        {
            _health.Died += Stop;
        }

        private void OnDisable()
        {
            _health.Died -= Stop;
        }

        private void Start()
        {
            Move();
        }

        private void Update()
        {
            if (Time.time >= _nextTimeToShoot && !_shoot)
            {
                _nextTimeToShoot = Time.time + 1f / _fireRate;

                StartCoroutine(Shoot());
            }
            else return;
        }

        private void OnDestroy()
        {
            _pause.UnRegister(this);
        }

        public void Die()
        {
            StartCoroutine(Deactivate());
        }

        public int GetDamage()
        {
            return _damage;
        }

        public void Move()
        {
            _enemyAnimation.MoveAnimatiom();

            _meshAgent.isStopped = false;

            _meshAgent.SetDestination(_player.transform.position);
        }

        public void Stop()
        {
            _meshAgent.isStopped = true;

            _shooting.enabled = false;

            Die();
        }

        public void StopAgent()
        {
            _meshAgent.isStopped = true;
        }

        public void UnStopAgent()
        {
            if (!_shoot)
            {
                _meshAgent.isStopped = false;
            }
            else return;
        }

        private IEnumerator Shoot()
        {
            _currentShootTime = _shootingTime;

            _meshAgent.isStopped = true;

            _shoot = true;

            _enemyAnimation.ShootAnimation();

            while (_currentShootTime > 0)
            {
                _currentShootTime -= Time.deltaTime;

                yield return null;
            }

            _currentShootTime = 0;

            _shoot = false;

            Move();
        }

        private IEnumerator Deactivate()
        {
            yield return new WaitForSeconds(_destroyTime);

            Destroy(_rootObject);
        }

        public void SetPause(bool paused)
        {
            enabled = !paused;

            _meshAgent.isStopped = paused;
        }
    }
}