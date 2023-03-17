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
            DontDestroyOnLoad(this);

            StartCoroutine(Initialize());
        }

        private IEnumerator Initialize()
        {
            LoadLanguage();

            yield return LocalizationSettings.InitializationOperation;

            switch (_language)
            {
                case Language.EN:
                    SetLanguage(Language.EN);
                    break;
                case Language.RU:
                    SetLanguage(Language.RU);
                    break;
                case Language.TR:
                    SetLanguage(Language.EN);
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

        private void LoadLanguage()
        {
            if (PlayerPrefs.HasKey(SettingName))
            {
                _language = (Language)PlayerPrefs.GetInt(SettingName);
            }
            else
            {
                _language = Language.EN;
            }
        }
    }
}