using System;

using UnityEngine;
using UnityEngine.UI;

namespace GameSettings
{
    public class BoolSettingView : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;

        public event Action<bool> SettingValueChanged = delegate { };

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(SettingValueClick);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(SettingValueClick);
        }

        public void Initialize(bool value)
        {
            _toggle.isOn = value;
        }

        private void SettingValueClick(bool value)
        {
            SettingValueChanged.Invoke(value);
        }
    }
}