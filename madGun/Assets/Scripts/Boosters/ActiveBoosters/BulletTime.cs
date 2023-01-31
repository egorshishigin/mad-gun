using System.Collections;

using Zenject;

using GamePause;

using UnityEngine;

namespace Boosters
{
    public class BulletTime : ActiveBoosterBase, IPauseHandler
    {
        [SerializeField] private float _slowdownFactor;

        private bool _slowDowned;

        private void Awake()
        {
            Pause.Register(this);
        }

        private void OnDestroy()
        {
            Pause.UnRegister(this);
        }

        public void SetPause(bool paused)
        {
            if (!paused && _slowDowned)
            {
                Time.timeScale = _slowdownFactor;

                Time.fixedDeltaTime = Time.timeScale * 0.02f;
            }
            else if (paused)
            {
                Time.timeScale = 0;
            }
            else if (!paused && !_slowDowned)
            {
                Time.timeScale = 1;
            }
        }

        protected override void OnActivated()
        {
            _slowDowned = true;

            Time.timeScale = _slowdownFactor;

            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }

        protected override void OnDectivated()
        {
            _slowDowned = false;

            Time.timeScale = 1;
        }
    }
}