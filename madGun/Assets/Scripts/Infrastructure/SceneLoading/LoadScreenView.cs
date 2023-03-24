using System;

using DG.Tweening;

using UnityEngine;

public class LoadScreenView : MonoBehaviour
{
    [SerializeField] private CanvasGroup _image;

    [SerializeField] private float _imageFadeDuration;

    [SerializeField] private float _imageFadeValue;

    [SerializeField] private RectTransform _loadIcon;

    [SerializeField] private Vector3 _rotateValue;

    [SerializeField] private float _iconAnimationDuration;

    [SerializeField] private RotateMode _rotateMode;

    [SerializeField] private LoopType _loopType;

    private Tween _fade;

    private Tween _loadRotation;

    public event Action FadeCompleted = delegate { };

    public void AnimateLoadScreen()
    {
        _fade = _image.DOFade(_imageFadeValue, _imageFadeDuration);

        _fade.OnComplete(() =>
        {
            _fade.Kill();

            FadeCompleted.Invoke();

            _loadRotation = _loadIcon.DOLocalRotate(_rotateValue,_iconAnimationDuration, _rotateMode).SetLoops(-1, _loopType);
        });
    }

    private void OnDestroy()
    {
        _fade.Kill();

        _loadRotation.Kill();
    }
}
