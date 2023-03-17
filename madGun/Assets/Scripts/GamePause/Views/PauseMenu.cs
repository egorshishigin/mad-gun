using Zenject;

using UnityEngine;

namespace GamePause
{
    public class PauseMenu : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private GameObject _menu;

        private Pause _pause;

        [Inject]
        private void Construct(Pause pause)
        {
            _pause = pause;

            _pause.Register(this);
        }

        public void SetPause(bool paused)
        {
            _menu.SetActive(paused);
        }

        private void OnDestroy()
        {
            _pause.UnRegister(this);
        }
    }
}