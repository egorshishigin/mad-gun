using Zenject;

using HealthSystem;

using GamePause;

using UnityEngine;

namespace Enemies
{
    public class JumpEnemyAnimation : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private Animator _animator;

        [SerializeField] private JumpEnemy _jumpEnemy;

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
            _jumpEnemy.Jumped += JumpAnimation;

            _health.Died += DisableAnimation;
        }

        private void OnDisable()
        {
            _jumpEnemy.Jumped -= JumpAnimation;

            _health.Died -= DisableAnimation;
        }

        private void OnDestroy()
        {
            _pause.UnRegister(this);
        }

        public void SetPause(bool paused)
        {
            _animator.speed = paused ? 0f : 1f;
        }

        private void JumpAnimation()
        {
            _animator.SetBool("Run", false);
        }

        private void DisableAnimation()
        {
            _animator.enabled = false;
        }
    }
}