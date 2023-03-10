using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Localization
{
    public class LocalizationSetting : MonoBehaviour
    {
        private const string SettingName = "Language";

        [DllImport("__Internal")]
        private static extern string GetLanguage();

        private string _language;

        private void Start()
        {
            StartCoroutine(Initialize());
        }

        private IEnumerator Initialize()
        {
            _language = GetLanguage();

            yield return LocalizationSettings.InitializationOperation;

            switch (_language)
            {
                case "ru":
                    SetLanguage(Language.RU);
                    break;
                case "tr":
                    SetLanguage(Language.TR);
                    break;
                default:
                    SetLanguage(Language.EN);
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