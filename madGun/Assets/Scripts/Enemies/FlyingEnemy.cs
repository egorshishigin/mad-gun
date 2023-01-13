using HealthSystem;
using System.Collections;

using UnityEngine;

namespace Enemies
{
    public class FlyingEnemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private float _speed;

        [SerializeField] private int _damage;

        [SerializeField] private float _destroyTime;

        [SerializeField] private Health _health;

        private void OnEnable()
        {
            _health.Died += Stop;
        }

        private void OnDisable()
        {
            _health.Died -= Stop;
        }

        private void FixedUpdate()
        {
            Move();
        }

        public int GetDamage()
        {
            return _damage;
        }

        public void Move()
        {
            _rigidbody.AddForce(Vector3.back * _speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }

        public void Stop()
        {
            _rigidbody.useGravity = true;

            StartCoroutine(Deactivate());
        }

        private IEnumerator Deactivate()
        {
            yield return new WaitForSeconds(_destroyTime);

            Destroy(gameObject);
        }
    }
}