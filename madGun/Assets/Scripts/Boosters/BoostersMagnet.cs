using UnityEngine;

namespace Boosters
{
    public class BoostersMagnet : MonoBehaviour, IUpgradetable
    {
        [SerializeField] private SphereCollider _collider;

        private float _magnitRadius;

        private void Start()
        {
            _collider.radius += _magnitRadius;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent<BoosterBase>(out BoosterBase booster))
            {
                booster.MoveToPlayer();
            }
            else return;
        }

        public void Upgrade(int timeAmount, int countAmount)
        {
            _magnitRadius += countAmount;
        }
    }
}