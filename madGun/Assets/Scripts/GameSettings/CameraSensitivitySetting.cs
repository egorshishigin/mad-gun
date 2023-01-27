using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSettings
{
    public class CameraSensitivitySetting : MonoBehaviour
    {
        private const string SettingName = "CameraSensitivity";

        private const float MaxSensitivity = 150f;

        private const float MinSensitivity = 15f;


        [SerializeField] private CameraSensitivitySettingView _view;

        private void OnEnable()
        {
            _view.SensitivityChanged += SensitivityChangedHandler;
        }

        private void OnDisable()
        {
            _view.SensitivityChanged -= SensitivityChangedHandler;
        }

        private void Awake()
        {
            float currentSensitivity = PlayerPrefs.GetFloat(SettingName);

            if (currentSensitivity < MinSensitivity)
            {
                currentSensitivity = MinSensitivity;
            }

            _view.InitializeView(MaxSensitivity, MinSensitivity, currentSensitivity);
        }

        private void SensitivityChangedHandler(float value)
        {
            PlayerPrefs.SetFloat(SettingName, value);

            PlayerPrefs.Save();
        }
    }
}