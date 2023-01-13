using HealthSystem;

using Enemies;

using UnityEngine;

namespace Player
{
    public class PlayerHitBox : MonoBehaviour, IPlayer, IShootable
    {
        [SerializeField] private Health _health;

        private IEnemy _enemy;

        public void HitHandler(int damage)
        {
            _health.ApplyDamage(damage);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.TryGetComponent(out _enemy))
            {
                _health.ApplyDamage(_enemy.GetDamage());
            }
        }
    }
}