using System.Runtime.InteropServices;

using Zenject;

using Data;

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class GameBootstrap : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern void LoadDataExtern();

        [SerializeField] private int TargetFPS;

        private GameDataIO _gameDataIO;

        private YandexAD _yandexAD;

        [Inject]
        private void Construct(GameDataIO gameDataIO, YandexAD yandexAD)
        {
            _gameDataIO = gameDataIO;

            _yandexAD = yandexAD;
        }

        private void Start()
        {
            SetTragetFrameRate();

            StartCoroutine(LoadScene(1));
        }

        public void LoadData(string data)
        {
            _gameDataIO.LoadGameData(data);
        }

        private void SetTragetFrameRate()
        {
            Application.targetFrameRate = TargetFPS;
        }

        private IEnumerator LoadScene(int buildIndex)
        {
            LoadDataExtern();

            _yandexAD.PlayFullScreenAD();

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(buildIndex);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}