using TMPro;

using HealthSystem;

using UnityEngine;

namespace Player
{
    public class PlayerHealthView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthText;

        [SerializeField] private Health _playerHealth;

        private void OnEnable()
        {
            _playerHealth.HealthChanged += UpdateHealthText;
        }

        private void OnDisable()
        {
            _playerHealth.HealthChanged -= UpdateHealthText;
        }

        private void UpdateHealthText(int value)
        {
            _healthText.text = value.ToString();
        }
    }
}