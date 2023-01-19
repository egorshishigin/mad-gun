using Zenject;

using Player;

using UnityEngine;

namespace Boosters
{
    public abstract class BoosterBase : MonoBehaviour, IBooster
    {
        [SerializeField] private BoosterType _type;

        [SerializeField] private BoostersHolder _boostersHolder;

        [SerializeField] private BoosterAnimation _animation;

        [SerializeField] private AudioSource _boosterSound;

        private PlayerHitBox _player;

        public BoosterType BoosterType => _type;

        public PlayerHitBox Player => _player;

        [Inject]
        private void Construct(PlayerHitBox playerHitBox)
        {
            _player = playerHitBox;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerHitBox>() != null)
            {
                Use();

                _animation.StopAnimation();

                _boostersHolder.ReturnToPool();
            }
            else return;
        }

        protected abstract void ActivateBooster();

        public void MoveToPlayer()
        {
            _animation.MoveAnimation(_player.transform);
        }

        public void Use()
        {
            _boosterSound.PlayOneShot(_boosterSound.clip);

            ActivateBooster();
        }
    }
}