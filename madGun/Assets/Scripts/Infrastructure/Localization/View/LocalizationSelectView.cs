using Zenject;

using UnityEngine;
using UnityEngine.UI;

namespace Localization
{
    public class LocalizationSelectView : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [SerializeField] private Language _language;

        private LocalizationSetting _localizationSetting;

        [Inject]
        private void Construct(LocalizationSetting localizationSetting)
        {
            _localizationSetting = localizationSetting;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(ChangeLanguage);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ChangeLanguage);
        }

        private void ChangeLanguage()
        {
            _localizationSetting.ChangeLanguage(_language);
        }
    }
}