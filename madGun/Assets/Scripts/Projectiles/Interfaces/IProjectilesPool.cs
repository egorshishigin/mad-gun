namespace Projectiles
{
    public interface IProjectilesPool
    {
        Projectile CreatePooledObject();

        void OnTakeFromPool(Projectile projectile);

        void OnReturnToPool(Projectile projectile);
    }
}