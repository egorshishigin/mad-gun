using System;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace GameSettings
{
    public class CameraSensitivitySettingView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _sensitivityValue;

        [SerializeField] private Slider _sensitivitySlider;

        public event Action<float> SensitivityChanged = delegate { };

        private void OnEnable()
        {
            _sensitivitySlider.onValueChanged.AddListener(OnSensitivitySliderChanged);
        }

        private void OnDisable()
        {
            _sensitivitySlider.onValueChanged.RemoveListener(OnSensitivitySliderChanged);
        }

        public void InitializeView(float maxValue, float minValue, float currentValue)
        {
            _sensitivitySlider.maxValue = maxValue;

            _sensitivitySlider.minValue = minValue;

            _sensitivitySlider.value = currentValue;

            UpdateSensitivityText(currentValue);
        }

        private void UpdateSensitivityText(float value)
        {
            _sensitivityValue.text = value.ToString();
        }

        private void OnSensitivitySliderChanged(float value)
        {
            SensitivityChanged.Invoke(value);

            UpdateSensitivityText(value);
        }
    }
}