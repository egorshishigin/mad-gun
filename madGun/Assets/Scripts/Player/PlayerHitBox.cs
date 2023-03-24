using HealthSystem;

using UnityEngine;

namespace Player
{
    public class PlayerHitBox : MonoBehaviour, IShootable
    {
        [SerializeField] private Health _health;

        public void HitHandler(int damage)
        {
            _health.ApplyDamage(damage);
        }

        public void Heal(int amount)
        {
            _health.HealUp(amount);
        }
    }
}