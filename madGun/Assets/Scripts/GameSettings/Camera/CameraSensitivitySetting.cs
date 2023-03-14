using UnityEngine;

namespace GameSettings
{
    public class CameraSensitivitySetting : MonoBehaviour
    {
        private const string SettingName = "CameraSensitivity";

        private const float MaxSensitivity = 150f;

        private const float MinSensitivity = 15f;

        private const float DefaultSensitivity = 45f;

        [SerializeField] private CameraSensitivitySettingView _view;

        private void OnEnable()
        {
            _view.SensitivityChanged += SensitivityChangedHandler;

            float currentSensitivity;

            if (PlayerPrefs.HasKey(SettingName))
            {
                currentSensitivity = PlayerPrefs.GetFloat(SettingName);
            }
            else
            {
                currentSensitivity = DefaultSensitivity;
            }

            SensitivityChangedHandler(currentSensitivity);

            _view.InitializeView(MaxSensitivity, MinSensitivity, currentSensitivity);
        }

        private void OnDisable()
        {
            _view.SensitivityChanged -= SensitivityChangedHandler;
        }

        private void SensitivityChangedHandler(float value)
        {
            PlayerPrefs.SetFloat(SettingName, value);

            PlayerPrefs.Save();
        }
    }
}