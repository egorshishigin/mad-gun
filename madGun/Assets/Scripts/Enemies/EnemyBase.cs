using System;
using System.Collections;

using Zenject;

using Player;

using HealthSystem;

using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public abstract class EnemyBase : MonoBehaviour, IEnemy
    {
        [SerializeField] private NavMeshAgent _meshAgent;

        [SerializeField] private Health _health;

        [SerializeField] private float _attackDistance;

        [SerializeField] private float _destroyTime;

        private float _distanceToPlayer;

        private PlayerHitBox _player;

        public PlayerHitBox Player => _player;

        public event Action Attacked = delegate { };

        [Inject]
        private void Consruct(PlayerHitBox player)
        {
            _player = player;
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

        private void Update()
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