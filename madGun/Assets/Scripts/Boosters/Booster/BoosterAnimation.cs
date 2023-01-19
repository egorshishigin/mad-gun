using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;

namespace Boosters
{
    public class BoosterAnimation : MonoBehaviour
    {
        [SerializeField] private float _duration;

        private Tween _tween;

        public void MoveAnimation(Transform player)
        {
            _tween = transform.DOMove(player.position, _duration);
        }

        public void StopAnimation()
        {
            _tween.Kill();
        }
    }
}