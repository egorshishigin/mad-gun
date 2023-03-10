using UnityEngine;

namespace GameSettings
{
    public abstract class BoolSetting : MonoBehaviour
    {
        [SerializeField] private string _settingName;

        [SerializeField] private BoolSettingView _view;

        private bool _value;

        public bool Value => _value;

        private void OnEnable()
        {
            _view.SettingValueChanged += SettingValueChangedHandler;
        }

        private void Start()
        {
            LoadSetting(_settingName);

            _view.Initialize(_value);

            ApplySetting(_value);
        }

        private void OnDisable()
        {
            _view.SettingValueChanged -= SettingValueChangedHandler;
        }

        private void SettingValueChangedHandler(bool value)
        {
            ApplySetting(value);

            SaveSetting(_settingName, value);
        }

        protected abstract void ApplySetting(bool value);

        private void SaveSetting(string name, bool value)
        {
            int settingValue = value ? 1 : 0;

            PlayerPrefs.SetInt(name, settingValue);
        }

        private void LoadSetting(string name)
        {
            int value;

            if (PlayerPrefs.HasKey(name))
            {
                value = PlayerPrefs.GetInt(name);
            }
            else
            {
                PlayerPrefs.SetInt(name, 1);

                value = 1;
            }

            _value = value != 0;
        }
    }
}