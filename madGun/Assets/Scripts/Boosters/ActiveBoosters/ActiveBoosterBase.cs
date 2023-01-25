using System.Collections;

using UnityEngine;

namespace Boosters
{
    public abstract class ActiveBoosterBase : MonoBehaviour
    {
        [SerializeField] private float _activeTime;

        public float ActiveTime => _activeTime;

        public void Activate()
        {
            gameObject.SetActive(true);

            StartCoroutine(StartBooster());
        }

        private void Deactivate()
        {
            OnDectivated();

            gameObject.SetActive(false);
        }

        protected abstract void OnActivated();

        protected abstract void OnDectivated();

        private IEnumerator StartBooster()
        {
            OnActivated();

            yield return new WaitForSeconds(_activeTime);

            Deactivate();
        }
    }
}