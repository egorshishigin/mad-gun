using Zenject;

using Player;

using HealthSystem;

using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Enemies
{
    public class WalkingEnemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private NavMeshAgent _meshAgent;

        [SerializeField] private Health _health;

        [SerializeField] private int _damage;

        [SerializeField] private float _destroyTime;

        [SerializeField] private GameObject _rootObject;

        private PlayerHitBox _player;

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

        private void OnDisable()
        {
            _health.Died -= Stop;
        }

        public void Move()
        {
            _meshAgent.SetDestination(_player.transform.position);
        }

        public void Stop()
        {
            _meshAgent.isStopped = true;

            StartCoroutine(Deactivate());
        }

        public int GetDamage()
        {
            return _damage;
        }

        private IEnumerator Deactivate()
        {
            yield return new WaitForSeconds(_destroyTime);

            Destroy(_rootObject);
        }

        public class Factory : PlaceholderFactory<IEnemy> { }
    }
}