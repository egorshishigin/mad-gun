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

        [SerializeField] private float _hitAnimationChance;

        private void OnEnable()
        {
            _health.Died += DisableAnimation;

            _health.Damaged += HealthChangedHandler;
        }

        private void OnDisable()
        {
            _health.Died -= DisableAnimation;

            _health.Damaged -= HealthChangedHandler;
        }

        public void HitAnimation()
        {
            float animationChance = Random.value;

            if (animationChance < _hitAnimationChance)
            {
                _animator.SetTrigger("Hit");

                _shootingEnemy.StopAgent();
            }
            else return;
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