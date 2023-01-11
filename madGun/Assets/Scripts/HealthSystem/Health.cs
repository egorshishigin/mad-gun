using System;

using UnityEngine;

namespace HealthSystem
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;

        private int _currentHealth;

        public event Action<int> HealthChanged = delegate { };

        public event Action Died = delegate { };

        private void Awake()
        {
            _currentHealth = _maxHealth;

            HealthChanged.Invoke(_currentHealth);
        }

        public void ApplyDamage(int damage)
        {
            _currentHealth -= damage;

            HealthChanged.Invoke(_currentHealth);

            if (_currentHealth <= 0)
            {
                Died.Invoke();
            }
            else return;
        }
    }
}