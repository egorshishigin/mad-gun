using Zenject;

using Localization;

using UnityEngine;

namespace Infrastructure
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoader _sceneLoader;

        [SerializeField] private LocalizationSetting _localizationSetting;

        public override void InstallBindings()
        {
            EnableCursor();

            BindSceneLoader();

            BindLocalizationSetting();
        }

        private static void EnableCursor()
        {
            Cursor.visible = true;

            Cursor.lockState = CursorLockMode.None;
        }

        private void BindSceneLoader()
        {
            Container
                .Bind<SceneLoader>()
                .FromInstance(_sceneLoader)
                .AsSingle();
        }

        private void BindLocalizationSetting()
        {
            Container
                .Bind<LocalizationSetting>()
                .FromInstance(_localizationSetting)
                .AsSingle();
        }
    }
}