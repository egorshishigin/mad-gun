using System.Collections.Generic;

using UnityEngine;

namespace Boosters
{
    public class BoostersPool
    {
        private readonly BoostersHolder.Pool _pool;

        private readonly List<BoostersHolder> _boostersHolders = new List<BoostersHolder>();

        public BoostersPool(BoostersHolder.Pool pool)
        {
            _pool = pool;
        }

        public void AddBooster(Vector3 startPosition, BoosterType type)
        {
            _boostersHolders.Add(_pool.Spawn(startPosition, type));
        }

        public void RemoveBooster(BoostersHolder boostersHolder)
        {
            _pool.Despawn(boostersHolder);

            _boostersHolders.Remove(boostersHolder);
        }
    }
}