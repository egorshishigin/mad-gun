using System;

using UnityEngine;

namespace HealthSystem
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;

        [SerializeField] private AudioSource _deathSound;

        private int _currentHealth;

        private bool _died = false;

        public event Action<int> HealthChanged = delegate { };

        public event Action Died = delegate { };

        public event Action Destroyed = delegate { };

        public int CurrentHealth => _currentHealth;

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        private void OnDestroy()
        {
            Destroyed.Invoke();
        }

        public void ApplyDamage(int damage)
        {
            _currentHealth -= damage;

            HealthChanged.Invoke(_currentHealth);

            if (_currentHealth <= 0 && !_died)
            {
                _died = true;

                if(_deathSound != null)
                {
                    _deathSound.Play();
                }

                Died.Invoke();

                enabled = false;
            }
            else return;
        }

        public void HealUp(int amount)
        {
            _currentHealth += amount;

            HealthChanged.Invoke(_currentHealth);
        }
    }
}