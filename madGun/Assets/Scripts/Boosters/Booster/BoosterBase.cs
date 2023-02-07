using Zenject;

using Player;

using UnityEngine;

namespace Boosters
{
    public abstract class BoosterBase : MonoBehaviour, IBooster
    {
        [SerializeField] private BoosterType _type;

        [SerializeField] private BoostersHolder _boostersHolder;

        [SerializeField] private BoosterAnimation _boosterAnimation;

        private BoostersAudio _audio;

        private PlayerHitBox _player;

        public BoosterType BoosterType => _type;

        public PlayerHitBox Player => _player;

        [Inject]
        private void Construct(PlayerHitBox playerHitBox, BoostersAudio boostersAudio)
        {
            _player = playerHitBox;

            _audio = boostersAudio;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerHitBox>() != null)
            {
                _audio.PlayBoosterAudio(_type);

                Use();

                _boosterAnimation.StopAnimation();

                _boostersHolder.ReturnToPool();
            }
        }

        protected abstract void ActivateBooster();

        public void Use()
        {
            ActivateBooster();
        }

        public void MoveToPlayer()
        {
            _boosterAnimation.MoveAnimation(_player.transform);
        }
    }
}