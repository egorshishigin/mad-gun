using System.Collections;

using Zenject;

using HealthSystem;

using GamePause;

using UnityEngine;

namespace Enemies
{
    public class FlyingEnemy : MonoBehaviour, IEnemy, IPauseHandler
    {
        [SerializeField] private Rigidbody _rigibody;

        [SerializeField] private float _speed;

        [SerializeField] private int _damage;

        [SerializeField] private float _destroyTime;

        [SerializeField] private Health _health;

        [SerializeField] private bool _move;

        private Pause _pause;

        [Inject]
        private void Consruct(Pause pause)
        {
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

        private void Update()
        {
            Move();
        }

        private void OnDestroy()
        {
            _pause.UnRegister(this);
        }

        public int GetDamage()
        {
            return _damage;
        }

        public void Move()
        {
            if (_move)
            {
                transform.Translate(Vector3.forward * _speed * Time.fixedDeltaTime);
            }
            else return;
        }

        public void Stop()
        {
            _move = false;

            _rigibody.useGravity = true;

            Die();
        }

        private IEnumerator Deactivate()
        {
            yield return new WaitForSeconds(_destroyTime);

            Destroy(gameObject);
        }

        public void Die()
        {
            StartCoroutine(Deactivate());
        }

        public void SetPause(bool paused)
        {
            _move = !paused;
        }
    }
}