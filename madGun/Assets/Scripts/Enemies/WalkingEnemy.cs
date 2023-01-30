using System.Collections;

using Zenject;

using Player;

using HealthSystem;

using GamePause;

using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class WalkingEnemy : MonoBehaviour, IEnemy, IPauseHandler
    {
        [SerializeField] private NavMeshAgent _meshAgent;

        [SerializeField] private Health _health;

        [SerializeField] private int _damage;

        [SerializeField] private float _destroyTime;

        [SerializeField] private GameObject _rootObject;

        private PlayerHitBox _player;

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

        private void Start()
        {
            Move();
        }

        private void OnDisable()
        {
            _health.Died -= Stop;
        }

        private void OnDestroy()
        {
            _pause.UnRegister(this);
        }

        public void Move()
        {
            _meshAgent.isStopped = false;

            _meshAgent.SetDestination(_player.transform.position);
        }

        public void HitHandler()
        {
            _meshAgent.isStopped = true;
        }

        public void Stop()
        {
            _meshAgent.isStopped = true;

            Die();
        }

        public void Die()
        {
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

        public void SetPause(bool paused)
        {
            enabled = !paused;

            _meshAgent.isStopped = paused;
        }
    }
}