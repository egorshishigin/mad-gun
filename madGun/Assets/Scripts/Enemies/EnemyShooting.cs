using Zenject;

using Player;

using UnityEngine;
using Projectiles;

namespace Enemies
{
    public class EnemyShooting : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;

        [SerializeField] private float _aimTime;

        [SerializeField] private float _speed;

        [SerializeField] private float _fireRate;

        private EnemyProjectilePool _projectilesPool;

        private PlayerHitBox _player;

        private float _nextTimeToShoot;

        [Inject]
        private void Construct(PlayerHitBox player, EnemyProjectilePool projectilesPool)
        {
            _player = player;

            _projectilesPool = projectilesPool;
        }

        private void Update()
        {
            Aim();

            if (Time.time >= _nextTimeToShoot)
            {
                _nextTimeToShoot = Time.time + 1f / _fireRate;

                Shoot();
            }
        }

        private void Shoot()
        {
            _projectilesPool.AddEnemyProjectile(_shootPoint.position, _shootPoint.forward, _speed);
        }

        private void Aim()
        {
            Quaternion rotation = Quaternion.LookRotation(_player.transform.position - _shootPoint.position);

            _shootPoint.rotation = Quaternion.Slerp(_shootPoint.rotation, rotation, Time.deltaTime * _aimTime);
        }
    }
}