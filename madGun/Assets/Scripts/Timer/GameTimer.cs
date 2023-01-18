using System;

using Zenject;

using UnityEngine;

namespace Timer
{
    public class GameTimer : ITickable
    {
        private float _gameTime;

        public float GameTime => _gameTime;

        public event Action<float> TimeChanged = delegate { };

        void ITickable.Tick()
        {
            _gameTime += Time.deltaTime;

            TimeChanged.Invoke(_gameTime);
        }
    }
}