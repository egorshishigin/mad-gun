using HealthSystem;

using UnityEngine;

namespace Enemies
{
    public class WallkingEnemyAnimation : MonoBehaviour, IEnemyAnimation
    {
        [SerializeField] private Animator _animator;

        [SerializeField] private Health _health;

        [SerializeField] private WalkingEnemy _walkingEnemy;

        private void OnEnable()
        {
            _health.Died += DisableAnimation;

            _health.HealthChanged += HealthChangedHandler;
        }

        private void HealthChangedHandler(int damage)
        {
            HitAnimation();
        }

        private void OnDisable()
        {
            _health.Died -= DisableAnimation;

            _health.HealthChanged -= HealthChangedHandler;
        }

        private void Start()
        {
            MoveAnimatiom();
        }

        public void HitAnimation()
        {
            _animator.SetBool("Walk", false);

            _walkingEnemy.HitHandler();
        }

        public void MoveAnimatiom()
        {
            _animator.SetBool("Walk", true);

            _walkingEnemy.Move();
        }

        private void DisableAnimation()
        {
            _animator.enabled = false;
        }
    }
}