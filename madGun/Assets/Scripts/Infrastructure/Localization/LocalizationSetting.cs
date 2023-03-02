using System.Collections;

using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Localization
{
    public class LocalizationSetting : MonoBehaviour
    {
        private const string SettingName = "Language";

        private Language _language;

        private void Start()
        {
            StartCoroutine(Initialize());
        }

        private IEnumerator Initialize()
        {
            _language = LoadLanguageSetting();

            yield return LocalizationSettings.InitializationOperation;

            switch (_language)
            {
                case Language.RU:
                    SetLanguage(_language);
                    break;
                case Language.TR:
                    SetLanguage(_language);
                    break;
                default:
                    SetLanguage(_language);
                    break;
            }
        }

        public void ChangeLanguage(Language language)
        {
            SetLanguage(language);
        }

        private void SetLanguage(Language language)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[(int)language];

            SaveSetting(language);
        }

        private void SaveSetting(Language language)
        {
            PlayerPrefs.SetInt(SettingName, (int)language);

            PlayerPrefs.Save();
        }

        private Language LoadLanguageSetting()
        {
            var languageSettingValue = PlayerPrefs.GetInt(SettingName);

            Language language = (Language)languageSettingValue;

            return language;
        }
    }
}