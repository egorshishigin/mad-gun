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
            _playerHealth.Damaged += UpdateHealthText;

            _playerHealth.HealthInitialized += UpdateHealthText;

            _playerHealth.Healed += UpdateHealthText;
        }

        private void OnDisable()
        {
            _playerHealth.Damaged -= UpdateHealthText;

            _playerHealth.HealthInitialized -= UpdateHealthText;

            _playerHealth.Healed -= UpdateHealthText;
        }

        private void UpdateHealthText(int value)
        {
            _healthText.text = value.ToString();
        }
    }
}