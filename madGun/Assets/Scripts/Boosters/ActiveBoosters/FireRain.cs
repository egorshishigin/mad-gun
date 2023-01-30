using UnityEngine;

namespace Boosters
{
    public class FireRain : ActiveBoosterBase
    {
        [SerializeField] private FireRainShooting _fireRainShooting;

        protected override void OnActivated()
        {
            _fireRainShooting.gameObject.SetActive(true);
        }

        protected override void OnDectivated()
        {
            _fireRainShooting.gameObject.SetActive(false);
        }
    }
}