using Zenject;

using UnityEngine;

namespace Infrastructure
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoader _sceneLoader;

        public override void InstallBindings()
        {
            Cursor.visible = true;

            Cursor.lockState = CursorLockMode.None;

            Container.Bind<SceneLoader>().FromInstance(_sceneLoader).AsSingle();
        }
    }
}