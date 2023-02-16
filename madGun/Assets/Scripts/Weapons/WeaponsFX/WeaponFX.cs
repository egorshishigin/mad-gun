using UnityEngine;

namespace Weapons
{
    public class WeaponFX : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;

        [SerializeField] private ParticleSystem _muzzleFlash;

        [SerializeField] private AudioSource _audio;

        private void OnEnable()
        {
            _weapon.Shot += PlayShotFX;
        }

        private void OnDisable()
        {
            _weapon.Shot -= PlayShotFX;
        }

        private void PlayShotFX()
        {
            PlayParticle();

            PlayAudio();
        }

        private void PlayParticle()
        {
            _muzzleFlash.Play();
        }

        private void PlayAudio()
        {
            if (_audio == null)
                return;

            _audio.Play();
        }
    }
}