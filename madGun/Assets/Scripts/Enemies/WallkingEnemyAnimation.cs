using Zenject;

using HealthSystem;

using GamePause;

using UnityEngine;

namespace Enemies
{
    public class WallkingEnemyAnimation : MonoBehaviour, IEnemyAnimation, IPauseHandler
    {
        [SerializeField] private Animator _animator;

        [SerializeField] private Health _health;

        [SerializeField] private WalkingEnemy _walkingEnemy;

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

        private void HealthChangedHandler(int damage)
        {
            HitAnimation();
        }

        private void OnDisable()
        {
            _health.Died -= DisableAnimation;

            _health.Dmaged -= HealthChangedHandler;
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
            _animator.SetBool("Walk", false);

            _walkingEnemy.HitHandler();
        }

        public void MoveAnimatiom()
        {
            _animator.SetBool("Walk", true);

            _walkingEnemy.Move();
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