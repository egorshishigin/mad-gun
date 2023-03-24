using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

namespace HealthSystem
{
    public class PlayerDamageFX : MonoBehaviour
    {
        [SerializeField] private Health _playerHealth;

        [SerializeField] private AudioSource _audio;

        [SerializeField] private Image _damageImage;

        [SerializeField] private float _duration;

        private Tween _imageAnimation;

        private void OnEnable()
        {
            _playerHealth.Damaged += HealthChangedHandler;
        }

        private void OnDisable()
        {
            _playerHealth.Damaged -= HealthChangedHandler;
        }

        private void OnDestroy()
        {
            _imageAnimation.Kill();
        }

        private void HealthChangedHandler(int obj)
        {
            _audio.PlayOneShot(_audio.clip);

            DamageImageAnimation();
        }

        private void DamageImageAnimation()
        {
            _imageAnimation = _damageImage.DOFade(255, _duration);

            _imageAnimation.OnComplete(() =>
            {
                _damageImage.DOFade(0, _duration);
            });
        }
    }
}