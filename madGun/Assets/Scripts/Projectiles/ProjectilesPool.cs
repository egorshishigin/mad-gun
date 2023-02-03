using UnityEngine;
using UnityEngine.Pool;

namespace Projectiles
{
    public class ProjectilesPool : MonoBehaviour
    {
        [SerializeField] private PlayerProjectile _projectilePrefab;

        private ObjectPool<PlayerProjectile> _pool;

        public ObjectPool<PlayerProjectile> Pool => _pool;

        private void Awake()
        {
            _pool = new ObjectPool<PlayerProjectile>(CreatePooledObject, OnTakeFromPool, OnReturnToPool);
        }

        private PlayerProjectile CreatePooledObject()
        {
            PlayerProjectile projectile = Instantiate(_projectilePrefab, transform);

            projectile.SetPool(_pool);

            return projectile;
        }

        private void OnTakeFromPool(PlayerProjectile projectile)
        {
            projectile.gameObject.SetActive(true);
        }

        private void OnReturnToPool(PlayerProjectile projectile)
        {
            projectile.gameObject.SetActive(false);

            projectile.ResetProjectile();
        }
    }
}