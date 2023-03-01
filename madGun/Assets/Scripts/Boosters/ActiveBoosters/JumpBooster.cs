using UnityEngine;

namespace Boosters
{
    public class JumpBooster : ActiveBoosterBase
    {
        [SerializeField] private float _jumpSpeed;

        public float JumpSpeed => _jumpSpeed;

        [HideInInspector]
        public bool CanJump;

        protected override void OnActivated()
        {
            CanJump = true;
        }

        protected override void OnDectivated()
        {
           
        }
    }
}