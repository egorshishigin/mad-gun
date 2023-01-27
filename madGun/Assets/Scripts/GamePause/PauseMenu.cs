using Zenject;

using UnityEngine;

namespace GamePause
{
    public class PauseMenu : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private GameObject _menu;

        [SerializeField] private GameObject _weapon;

        private Pause _pause;

        [Inject]
        private void Construct(Pause pause)
        {
            _pause = pause;

            _pause.Register(this);
        }

        private void Awake()
        {
            Cursor.visible = false;

            Cursor.lockState = CursorLockMode.Locked;
        }

        public void SetPause(bool paused)
        {
            _menu.SetActive(paused);

            _weapon.SetActive(!paused);

            if (paused)
            {
                Cursor.visible = true;

                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;

                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}