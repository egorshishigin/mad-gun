using System;

using Zenject;

using Player;

using Projectiles;

using UnityEngine;

namespace Enemies
{
    public class EnemyShooting : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;

        [SerializeField] private float _speed;

        private EnemyProjectilePool _projectilesPool;

        private PlayerHitBox _player;

        [Inject]
        private void Construct(PlayerHitBox player, EnemyProjectilePool projectilesPool)
        {
            _player = player;

            _projectilesPool = projectilesPool;
        }

        public void Shoot()
        {
            Aim();

            LaunchProjectile();
        }

        private void LaunchProjectile()
        {
            _projectilesPool.AddEnemyProjectile(_shootPoint.position, _shootPoint.forward, _speed);
        }

        private void Aim()
        {
            _shootPoint.LookAt(_player.transform.position);
        }
    }
}