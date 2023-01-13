using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HealthSystem
{
    public class HeadHitBox : MonoBehaviour, IShootable
    {
        [SerializeField] private Health _health;

        [SerializeField] private int _headShotMultiplier;

        public event Action HeadShot = delegate { };

        public void HitHandler(int damage)
        {
            if (_health.CurrentHealth > 0)
            {
                _health.ApplyDamage(damage * _headShotMultiplier);

                HeadShot.Invoke();
            }
            else
            {
                enabled = false;
            }
        }
    }
}