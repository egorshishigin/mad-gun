using HealthSystem;

using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class EnemyBaseAnimation : MonoBehaviour, IEnemyAnimation
    {
        private const string AttackName = "Attack";

        private const string MoveName = "Move";

        private const string HitName = "Hit";

        [SerializeField] private EnemyBase _enemy;

        [SerializeField] private Health _health;

        [SerializeField] private Animator _animator;

        [SerializeField] private float _hitAnimationChance;

        private void OnEnable()
        {
            _health.Died += DisableAnimation;

            _health.Damaged += HealthChangedHandler;

            _enemy.Attacked += AttackAnimation;
        }

        private void OnDisable()
        {
            _health.Died -= DisableAnimation;

            _health.Damaged -= HealthChangedHandler;

            _enemy.Attacked -= AttackAnimation;
        }

        private void AttackAnimation()
        {
            _animator.SetBool(MoveName, false);

            _animator.SetBool(AttackName, true);
        }

        private void HealthChangedHandler(int damage)
        {
            HitAnimation();
        }

        private void DisableAnimation()
        {
            _animator.enabled = false;
        }

        public void HitAnimation()
        {
            float animationChance = Random.value;

            if (animationChance < _hitAnimationChance)
            {
                _animator.SetBool(MoveName, false);

                _animator.SetBool(AttackName, false);

                _animator.SetTrigger(HitName);
            }
            else return;
        }

        public void MoveAnimatiom()
        {
            _animator.SetBool(MoveName, true);

            _animator.SetBool(AttackName, false);
        }
    }
}