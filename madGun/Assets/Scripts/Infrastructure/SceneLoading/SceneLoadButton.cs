using Zenject;

using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure
{
    public class SceneLoadButton : MonoBehaviour
    {
        [SerializeField] private int _sceneIndex;

        [SerializeField] private Button _button;

        [SerializeField] private LoadScreenView _loadScreenView;

        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(StartLoadScreenAnimation);

            _loadScreenView.FadeCompleted += LoadScene;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(StartLoadScreenAnimation);

            _loadScreenView.FadeCompleted += LoadScene;
        }

        private void StartLoadScreenAnimation()
        {
            _loadScreenView.gameObject.SetActive(true);

            _loadScreenView.AnimateLoadScreen();
        }

        private void LoadScene()
        {
            _sceneLoader.LoadSceneAsync(_sceneIndex);
        }
    }
}