using UnityEngine;

namespace HealthSystem
{
    public class HitBox : MonoBehaviour, IShootable
    {
        [SerializeField] private Health _health;

        public void HitHandler(int damage)
        {
            if (_health.CurrentHealth <= 0)
            {
                enabled = false;
            }

            _health.ApplyDamage(damage);
        }
    }
}