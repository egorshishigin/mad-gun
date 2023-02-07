using UnityEngine;

namespace Enemies
{
    public class ExplodingEnemy : EnemyBase
    {
        [SerializeField] private EnemyExplosion _enemyExplosion;

        protected override void Attack()
        {
            _enemyExplosion.DiedExplosion();
        }
    }
}