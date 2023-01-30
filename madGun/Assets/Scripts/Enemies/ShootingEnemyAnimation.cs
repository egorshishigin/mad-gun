using Zenject;

using HealthSystem;

using GamePause;

using UnityEngine;

namespace Enemies
{
    public class ShootingEnemyAnimation : MonoBehaviour, IEnemyAnimation, IPauseHandler
    {
        [SerializeField] private Animator _animator;

        [SerializeField] private Health _health;

        [SerializeField] private ShootingEnemy _shootingEnemy;

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

            _health.Dmaged += HealthChangedHandler;
        }

        private void OnDisable()
        {
            _health.Died -= DisableAnimation;

            _health.Dmaged -= HealthChangedHandler;
        }

        private void OnDestroy()
        {
            _pause.UnRegister(this);
        }

        public void HitAnimation()
        {
            _animator.SetTrigger("Hit");

            _shootingEnemy.StopAgent();
        }

        public void ContinueMove()
        {
            _shootingEnemy.UnStopAgent();
        }

        public void MoveAnimatiom()
        {
            _animator.SetBool("Run", true);

            _animator.SetBool("Shot", false);
        }

        public void ShootAnimation()
        {
            _animator.SetBool("Run", false);

            _animator.SetBool("Shot", true);
        }

        public void SetPause(bool paused)
        {
            _animator.speed = paused ? 0f : 1f;
        }

        private void HealthChangedHandler(int damage)
        {
            HitAnimation();
        }

        private void DisableAnimation()
        {
            _animator.enabled = false;
        }
    }
}