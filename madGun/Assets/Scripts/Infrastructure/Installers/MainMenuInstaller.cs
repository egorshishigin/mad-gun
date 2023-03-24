using Zenject;

using UnityEngine;

namespace Infrastructure
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoader _sceneLoader;

        public override void InstallBindings()
        {
            BindSceneLoader();
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