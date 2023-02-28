using System.Collections;

using Zenject;

using Player;

using HealthSystem;

using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class ShootingEnemy : MonoBehaviour, IEnemy, IUpdatable
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

        [SerializeField] private float _lookDamping;

        private PlayerHitBox _player;

        private UpdatesContainer _updatesContainer;

        private float _nextTimeToShoot;

        private bool _shoot = false;

        private float _currentShootTime;

        [Inject]
        private void Construct(PlayerHitBox player, UpdatesContainer updatesContainer)
        {
            _player = player;

            _updatesContainer = updatesContainer;

            _updatesContainer.Register(this);
        }

        void IUpdatable.Run()
        {
            if (!_shooting.enabled)
                return;

            if (Time.time >= _nextTimeToShoot && !_shoot)
            {
                _nextTimeToShoot = Time.time + 1f / _fireRate;

                StartCoroutine(Shoot());
            }
            else return;
        }

        private void OnEnable()
        {
            _health.Died += Stop;

            _health.GoreDied += Stop;
        }

        private void OnDisable()
        {
            _health.Died -= Stop;

            _health.GoreDied -= Stop;
        }

        private void Start()
        {
            Move();
        }

        public void Die()
        {
            StartCoroutine(Deactivate());
        }

        public void Move()
        {
            _enemyAnimation.MoveAnimatiom();

            _meshAgent.isStopped = false;

            _meshAgent.SetDestination(_player.transform.position);
        }

        public void Stop()
        {
            StopAllCoroutines();

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

                var lookPosition = _player.transform.position - _rootObject.transform.position;

                lookPosition.y = 0;

                var lookRotation = Quaternion.LookRotation(lookPosition);

                _rootObject.transform.rotation = Quaternion.Slerp(_rootObject.transform.rotation, lookRotation, Time.deltaTime * _lookDamping);

                yield return null;
            }

            _currentShootTime = 0;

            _shoot = false;

            Move();
        }

        private IEnumerator Deactivate()
        {
            _updatesContainer.UnRegister(this);

            yield return new WaitForSeconds(_destroyTime);

            Destroy(_rootObject);
        }
    }
}