using System;

using Zenject;

using GamePause;

using UnityEngine;

namespace Timer
{
    public class GameTimer : ITickable, IPauseHandler, IDisposable
    {
        private float _gameTime;

        private bool _timePaused;

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
            _timePaused = paused;
        }

        void ITickable.Tick()
        {
            if (_timePaused)
                return;

            _gameTime += Time.deltaTime;

            TimeChanged.Invoke(_gameTime);
        }

        void IDisposable.Dispose()
        {
            _pause.UnRegister(this);
        }
    }
}