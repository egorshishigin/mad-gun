using UnityEngine;

namespace Boosters
{
    public class Medkit : BoosterBase, IUpgradetable
    {
        [SerializeField] private int _healAmount;

        public void Upgrade(int amount)
        {
            _healAmount += amount;
        }

        protected override void ActivateBooster()
        {
            Player.Heal(_healAmount);
        }
    }
}