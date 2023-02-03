using HealthSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            _animator.SetBool(MoveName, false);

            _animator.SetBool(AttackName, false);

            _animator.SetTrigger(HitName);
        }

        public void MoveAnimatiom()
        {
            _animator.SetBool(MoveName, true);

            _animator.SetBool(AttackName, false);
        }
    }
}