using System.Collections;

using Zenject;

using Data;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private int TargetFPS;

        private GameDataIO _gameDataIO;

        [Inject]
        private void Construct(GameDataIO gameDataIO)
        {
            _gameDataIO = gameDataIO;
        }

        private void Start()
        {
            SetTragetFrameRate();

            StartCoroutine(LoadScene(1));
        }

        private void SetTragetFrameRate()
        {
            Application.targetFrameRate = TargetFPS;
        }

        private IEnumerator LoadScene(int buildIndex)
        {
            _gameDataIO.LoadGameData();

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(buildIndex);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}