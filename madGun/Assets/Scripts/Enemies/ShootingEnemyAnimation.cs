using Zenject;

using HealthSystem;

using GamePause;

using UnityEngine;

namespace Enemies
{
    public class ShootingEnemyAnimation : MonoBehaviour, IEnemyAnimation
    {
        [SerializeField] private Animator _animator;

        [SerializeField] private Health _health;

        [SerializeField] private ShootingEnemy _shootingEnemy;

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