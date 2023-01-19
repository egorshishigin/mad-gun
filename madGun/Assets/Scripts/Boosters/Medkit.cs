using UnityEngine;

namespace Boosters
{
    public class Medkit : BoosterBase
    {
        [SerializeField] private int _healAmount;

        protected override void ActivateBooster()
        {
            Player.Heal(_healAmount);
        }
    }
}