using Zenject;

using Localization;

using UnityEngine;

namespace Infrastructure
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoader _sceneLoader;

        public override void InstallBindings()
        {
            EnableCursor();

            BindSceneLoader();
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
    }
}