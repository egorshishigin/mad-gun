using HealthSystem;

using UnityEngine;

namespace Enemies
{
    public class JumpEnemyAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        [SerializeField] private JumpEnemy _jumpEnemy;

        [SerializeField] private Health _health;

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