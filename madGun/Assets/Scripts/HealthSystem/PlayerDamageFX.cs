using System.Collections;
using System.Collections.Generic;

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

        private void OnEnable()
        {
            _playerHealth.Damaged += HealthChangedHandler;
        }

        private void OnDisable()
        {
            _playerHealth.Damaged -= HealthChangedHandler;
        }

        private void HealthChangedHandler(int obj)
        {
            _audio.PlayOneShot(_audio.clip);

            DamageImageAnimation();
        }

        private void DamageImageAnimation()
        {
            Tween tween = _damageImage.DOFade(255, _duration);

            tween.OnComplete(() =>
            {
                _damageImage.DOFade(0, _duration);
            });
        }
    }
}