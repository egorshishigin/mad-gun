using UnityEngine;

namespace Boosters
{
    public class BoostersAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private AudioClip _coin;

        [SerializeField] private AudioClip _medKit;

        [SerializeField] private AudioClip _ammo;

        public void PlayBoosterAudio(BoosterType boosterType)
        {
            switch (boosterType)
            {
                case BoosterType.Coin:
                    PlayAudio(_coin);
                    break;
                case BoosterType.Medkit:
                    PlayAudio(_medKit);
                    break;
                case BoosterType.Ammo:
                    PlayAudio(_ammo);
                    break;
            }
        }

        private void PlayAudio(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }
    }
}