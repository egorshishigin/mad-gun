using System;

using UnityEngine;

namespace HealthSystem
{
    public class Health : MonoBehaviour, IUpgradetable
    {
        [SerializeField] private int _maxHealth;

        [SerializeField] private AudioSource _deathSound;

        private int _currentHealth;

        private bool _died = false;

        public event Action<int> Damaged = delegate { };

        public event Action<int> HealthInitialized = delegate { };

        public event Action<int> Healed = delegate { };

        public event Action Died = delegate { };

        public event Action Destroyed = delegate { };

        public int CurrentHealth => _currentHealth;

        private void Start()
        {
            _currentHealth = _maxHealth;

            HealthInitialized.Invoke(_currentHealth);
        }

        private void OnDestroy()
        {
            Destroyed.Invoke();
        }

        public void ApplyDamage(int damage)
        {
            _currentHealth -= damage;

            Damaged.Invoke(_currentHealth);

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

            Healed.Invoke(_currentHealth);
        }

        public void Upgrade(int amount, int count)
        {
            _maxHealth += amount;
        }
    }
}