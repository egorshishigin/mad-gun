using System;
using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

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

    private Tween _loadRotation;

    public event Action FadeCompleted = delegate { };

    public void AnimateLoadScreen()
    {
        Tween tween = _image.DOFade(_imageFadeValue, _imageFadeDuration);

        tween.OnComplete(() =>
        {
            tween.Kill();

            FadeCompleted.Invoke();

            _loadRotation = _loadIcon.DOLocalRotate(_rotateValue,_iconAnimationDuration, _rotateMode).SetLoops(-1, _loopType);
        });
    }

    private void OnDestroy()
    {
        _loadRotation.Kill();
    }
}
