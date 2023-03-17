using System;
using System.Collections;

using Zenject;

using GamePause;

using PlayerInput;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Boosters
{
    public abstract class ActiveBoosterBase : MonoBehaviour, IUpgradetable
    {
        [SerializeField] private float _activeTime;

        [SerializeField] private float _cooldown;

        [SerializeField] private int _count;

        [SerializeField] private InputActionReference _boosterAction;

        [SerializeField] private PlayerInputActions.PlayerActions _actions;

        private bool _used;

        private Pause _pause;

        private PlayerControl _playerControl;

        public float ActiveTime => _activeTime;

        public int Count => _count;

        public Pause Pause => _pause;

        public event Action<float> Cooldowned = delegate { };

        public event Action<int> BoosterUsed = delegate { };

        public event Action Refreshed = delegate { };

        [Inject]
        private void Construct(Pause pause, PlayerControl playerControl)
        {
            _pause = pause;

            _playerControl = playerControl;
        }

        private void OnEnable()
        {
            _boosterAction.action.Enable();

            _boosterAction.action.performed += _ => Use();
        }

        private void OnDisable()
        {
            _boosterAction.action.performed -= _ => Use();
        }

        private void Use()
        {
            if (!_used && _count > 0)
            {
                Activate();
            }
            else return;
        }

        public void Upgrade(int timeAmount, int countAmount)
        {
            _activeTime += timeAmount;

            _count += countAmount;
        }

        public void Activate()
        {
            StartCoroutine(StartBooster());
        }

        private void Deactivate()
        {
            OnDectivated();
        }

        protected abstract void OnActivated();

        protected abstract void OnDectivated();

        private IEnumerator StartBooster()
        {
            if (_pause.Paused)
                yield return null;

            _used = true;

            _count--;

            BoosterUsed.Invoke(_count);

            StartCoroutine(Cooldown());

            OnActivated();

            yield return new WaitForSeconds(_activeTime);

            Deactivate();
        }

        private IEnumerator Cooldown()
        {
            float time = _cooldown;

            while (time > 0)
            {
                if (!_pause.Paused)
                {
                    time -= Time.deltaTime;

                    Cooldowned.Invoke(time);
                }

                yield return null;
            }

            _used = false;

            Refreshed.Invoke();
        }
    }
}