using Zenject;

using HealthSystem;

using GamePause;

using UnityEngine;

namespace Enemies
{
    public class FlyingEnemyAnimation : MonoBehaviour, IEnemyAnimation, IPauseHandler
    {
        [SerializeField] private Animator _animator;

        [SerializeField] private Health _health;

        private Pause _pause;

        [Inject]
        private void Construct(Pause pause)
        {
            _pause = pause;

            _pause.Register(this);
        }

        private void OnEnable()
        {
            _health.Died += DisableAnimation;
        }

        private void OnDisable()
        {
            _health.Died -= DisableAnimation;
        }

        private void Start()
        {
            MoveAnimatiom();
        }

        private void OnDestroy()
        {
            _pause.UnRegister(this);
        }

        public void HitAnimation()
        {

        }

        public void MoveAnimatiom()
        {
            _animator.SetBool("Fly", true);
        }

        public void SetPause(bool paused)
        {
            _animator.speed = paused ? 0f : 1f;
        }

        private void DisableAnimation()
        {
            _animator.enabled = false;
        }
    }
}