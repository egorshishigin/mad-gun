namespace Enemies
{
    public interface IEnemy
    {
        void Move();

        void Stop();

        int GetDamage();

        void Die();
    }
}