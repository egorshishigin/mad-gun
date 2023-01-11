using TMPro;

using DG.Tweening;

using UnityEngine;

namespace HealthSystem
{
    public class HeadShotView : MonoBehaviour
    {
        [SerializeField] private HitBox _headHitBox;

        [SerializeField] private TMP_Text _headShotText;

        [SerializeField] private float _duration;

        private Tween _textAnimation;

        private void OnEnable()
        {
            _headHitBox.HeadShot += HeadShotHandler;
        }

        private void OnDisable()
        {
            _headHitBox.HeadShot -= HeadShotHandler;
        }

        private void HeadShotHandler()
        {
            PlayTextAnimation();
        }

        private void PlayTextAnimation()
        {
            _textAnimation = _headShotText.DOFade(1, _duration);

            _textAnimation.OnComplete(() =>
            {
                _textAnimation.Kill();

                _headShotText.DOFade(0, 0);

                enabled = false;
            });
        }
    }
}