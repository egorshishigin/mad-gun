using System;
using System.Collections;

using Zenject;

using Player;

using HealthSystem;

using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public abstract class EnemyBase : MonoBehaviour, IEnemy, IUpdatable
    {
        [SerializeField] private NavMeshAgent _meshAgent;

        [SerializeField] private Health _health;

        [SerializeField] private float _attackDistance;

        [SerializeField] private float _destroyTime;

        [SerializeField] private AudioSource _attackSound;

        private float _distanceToPlayer;

        private PlayerHitBox _player;

        private UpdatesContainer _updatesContainer;

        public PlayerHitBox Player => _player;

        public event Action Attacked = delegate { };

        [Inject]
        private void Consruct(PlayerHitBox player, UpdatesContainer updatesContainer)
        {
            _player = player;

            _updatesContainer = updatesContainer;

            _updatesContainer.Register(this);
        }

        void IUpdatable.Run()
        {
            _distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

            if (_distanceToPlayer <= _attackDistance)
            {
                StopAgent();

                Attacked.Invoke();
            }
            else
            {
                Move();
            }
        }

        private void OnEnable()
        {
            _health.Died += Stop;
        }

        private void Start()
        {
            UnStopAgent();

            Move();
        }

        private void OnDestroy()
        {
            _updatesContainer.UnRegister(this);
        }

        private void OnDisable()
        {
            _health.Died -= Stop;
        }

        protected abstract void Attack();

        public void Die()
        {
            StartCoroutine(Deactivate());
        }

        public void Move()
        {
            _meshAgent.SetDestination(_player.transform.position);
        }

        public void Stop()
        {
            StopAgent();

            Die();
        }

        private void StopAgent()
        {
            _meshAgent.isStopped = true;
        }

        public void PlayAttackSound()
        {
            _attackSound.PlayOneShot(_attackSound.clip);
        }

        private void UnStopAgent()
        {
            _meshAgent.isStopped = false;

            _meshAgent.SetDestination(_player.transform.position);
        }

        private IEnumerator Deactivate()
        {
            yield return new WaitForSeconds(_destroyTime);

            Destroy(gameObject);
        }
    }
}