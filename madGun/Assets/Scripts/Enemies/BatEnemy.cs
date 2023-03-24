using System.Collections;

using Zenject;

using Player;

using HealthSystem;

using UnityEngine;

namespace Enemies
{
    public class BatEnemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private Health _health;

        [SerializeField] private int _damage;

        [SerializeField] private float _speed;

        [SerializeField] private float _destroyTime;

        [SerializeField] private Animator _animator;

        private bool _move;

        private PlayerHitBox _player;

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
            _move = true;

            _rigidbody.isKinematic = false;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerHitBox playerHitBox))
            {
                playerHitBox.HitHandler(_damage);

                Stop();
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

        public void Move()
        {
            if (!_move)
                return;

            transform.LookAt(_player.transform.position);

            _rigidbody.AddForce(transform.forward * _speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }

        public void Stop()
        {
            _animator.enabled = false;

            _rigidbody.useGravity = true;

            _move = false;

            Die();
        }

        private IEnumerator Deactivate()
        {
            yield return new WaitForSeconds(_destroyTime);

            Destroy(gameObject);
        }
    }
}