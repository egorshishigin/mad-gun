using System.Collections;

using Zenject;

using UnityEngine;

namespace Boosters
{
    public class BulletTime : ActiveBoosterBase
    {
        [SerializeField] private float _slowdownFactor;

        private ActiveBoostersState _boostersState;

        [Inject]
        private void Construct(ActiveBoostersState activeBoostersState)
        {
            _boostersState = activeBoostersState;
        }

        protected override void OnActivated()
        {
            _boostersState.BulletTime = true;

            StartCoroutine(SlowDownTime());
        }

        protected override void OnDectivated()
        {
            _boostersState.BulletTime = false;
        }

        private IEnumerator SlowDownTime()
        {
            Time.timeScale = _slowdownFactor;

            Time.fixedDeltaTime = Time.timeScale * 0.02f;

            yield return new WaitForSeconds(ActiveTime);

            Time.timeScale = 1;
        }
    }
}