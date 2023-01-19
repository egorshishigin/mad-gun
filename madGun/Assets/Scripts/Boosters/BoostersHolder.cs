using System.Linq;

using Zenject;

using UnityEngine;
using System.Collections.Generic;

namespace Boosters
{
    public class BoostersHolder : MonoBehaviour
    {
        [SerializeField] private List<BoosterBase> _boosters;

        private BoostersPool _boostersPool;

        [Inject]
        private void Construct(BoostersPool boostersPool)
        {
            _boostersPool = boostersPool;
        }

        public void ReturnToPool()
        {
            _boostersPool.RemoveBooster();
        }

        private void ActivateBooster(Vector3 startPosition, BoosterType type)
        {
            BoosterBase booster = _boosters.FirstOrDefault(item => item.BoosterType == type);

            booster.gameObject.SetActive(true);

            booster.transform.position = startPosition;

            booster.MoveToPlayer();
        }

        public class Pool : MonoMemoryPool<Vector3, BoosterType, BoostersHolder>
        {
            protected override void OnCreated(BoostersHolder item)
            {
                item.gameObject.SetActive(false);
            }

            protected override void Reinitialize(Vector3 position, BoosterType type, BoostersHolder item)
            {
                item.gameObject.SetActive(true);

                item.ActivateBooster(position, type);
            }

            protected override void OnDespawned(BoostersHolder item)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}