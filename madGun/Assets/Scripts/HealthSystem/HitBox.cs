using System;

using UnityEngine;

namespace HealthSystem
{
    public class HitBox : MonoBehaviour, IShootable
    {
        [SerializeField] private Health _health;

        [SerializeField] private bool _head;

        public event Action HeadShot = delegate { };

        public void HitHandler(int damage)
        {
            int amount;

            if (_head)
            {
                amount = damage * 10;

                HeadShot.Invoke();
            }
            else
            {
                amount = damage;
            }

            _health.ApplyDamage(amount);
        }
    }
}