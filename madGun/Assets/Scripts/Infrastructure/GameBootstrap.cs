using System.Collections;
using System.Runtime.InteropServices;

using Zenject;

using DG.Tweening;

using Data;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class GameBootstrap : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern void LoadDataExtern();

        [SerializeField] private int TargetFPS;

        [SerializeField] private float _loadDelay;

        [SerializeField] private Transform _loadIcon;

        private Tween _loadTween;

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

#if UNITY_STANDALONE || UNITY_EDITOR
            _gameDataIO.LoadLocalData();
#else
            LoadDataExtern();

            _yandexAD.PlayFullScreenAD();
#endif

            StartCoroutine(LoadScene());
        }

        public void LoadData(string data)
        {
            _gameDataIO.LoadGameData(data);
        }

        private void SetTragetFrameRate()
        {
            Application.targetFrameRate = TargetFPS;
        }

        private IEnumerator LoadScene()
        {
            _loadTween = _loadIcon.DOLocalRotate(new Vector3(0, 0, -360f), 1f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental);

            yield return new WaitForSeconds(_loadDelay);

            _loadTween.Kill();

            SceneManager.LoadScene(1);
        }
    }
}