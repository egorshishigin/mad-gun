using System;
using System.Collections;

using Zenject;

using HealthSystem;
using Player;

using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class JumpEnemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private NavMeshAgent _meshAgent;

        [SerializeField] private Health _health;

        [SerializeField] private int _damage;

        [SerializeField] private float _destroyTime;

        [SerializeField] private GameObject _rootObject;

        [SerializeField] private float _jumpDistance;

        private PlayerHitBox _player;

        private float _dictanceToPlayer;

        public event Action Jumped = delegate { };

        [Inject]
        private void Construct(PlayerHitBox player)
        {
            _player = player;
        }

        private void OnEnable()
        {
            _health.Died += Stop;
        }

        private void Start()
        {
            Move();
        }

        private void Update()
        {
            _dictanceToPlayer = (transform.position - _player.transform.position).magnitude;

            if (_dictanceToPlayer <= _jumpDistance)
            {
                Jump();
            }
        }

        private void OnDisable()
        {
            _health.Died -= Stop;
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
            _meshAgent.isStopped = false;

            _meshAgent.SetDestination(_player.transform.position);
        }

        public void Stop()
        {
            _meshAgent.isStopped = true;

            Die();
        }

        private void Jump()
        {
            _meshAgent.isStopped = true;

            //_meshAgent.enabled = false;

            Jumped.Invoke();
        }

        private IEnumerator Deactivate()
        {
            yield return new WaitForSeconds(_destroyTime);

            Destroy(_rootObject);
        }
    }
}