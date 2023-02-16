using Zenject;

using UnityEngine;
using UnityEngine.UI;

namespace GamePause
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [SerializeField] private bool _pauseValue;

        private Pause _pause;

        [Inject]
        private void Construct(Pause pause)
        {
            _pause = pause;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(PauseGame);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(PauseGame);
        }

        private void PauseGame()
        {
            _pause.SetPause(_pauseValue);
        }
    }
}