using Zenject;

using PlayerInput;

using UnityEngine;

namespace GamePause
{
    public class KeyBoardPauseButton : MonoBehaviour
    {
        private Pause _pause;

        private PlayerControl _playerControl;

        [Inject]
        private void Construct(Pause pause, PlayerControl playerControl)
        {
            _pause = pause;

            _playerControl = playerControl;
        }

        private void OnEnable()
        {
            _playerControl.PauseButtonDown += PauseButtonDownHandler;
        }

        private void OnDisable()
        {
            _playerControl.PauseButtonDown -= PauseButtonDownHandler;
        }

        private void PauseButtonDownHandler()
        {
            _pause.SetPause(true);
        }
    }
}