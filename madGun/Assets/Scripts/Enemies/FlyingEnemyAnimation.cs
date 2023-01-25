using HealthSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class FlyingEnemyAnimation : MonoBehaviour, IEnemyAnimation
    {
        [SerializeField] private Animator _animator;

        [SerializeField] private Health _health;

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

        public void HitAnimation()
        {

        }

        public void MoveAnimatiom()
        {
            _animator.SetBool("Fly", true);
        }

        private void DisableAnimation()
        {
            _animator.enabled = false;
        }
    }
}