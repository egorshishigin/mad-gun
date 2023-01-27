using Projectiles;

public class WeaponSettings
{
    private PlayerProjectile _playerProjectile;

    public PlayerProjectile PlayerProjectile => _playerProjectile;

    public void SetWeaponPrefab(PlayerProjectile projectile)
    {
        _playerProjectile = projectile;
    }
}
