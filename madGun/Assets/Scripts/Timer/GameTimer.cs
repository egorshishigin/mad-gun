using System;

using Zenject;

using GamePause;

using UnityEngine;

namespace Timer
{
    public class GameTimer : ITickable, IPauseHandler
    {
        private float _gameTime;

        private Pause _pause;

        public float GameTime => _gameTime;

        public event Action<float> TimeChanged = delegate { };

        [Inject]
        public GameTimer(Pause pause)
        {
            _pause = pause;

            _pause.Register(this);
        }

        public void SetPause(bool paused)
        {
            Time.timeScale = paused ? 0f : 1f;
        }

        void ITickable.Tick()
        {
            _gameTime += Time.deltaTime;

            TimeChanged.Invoke(_gameTime);
        }
    }
}