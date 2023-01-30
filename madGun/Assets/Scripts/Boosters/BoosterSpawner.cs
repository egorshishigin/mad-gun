using Zenject;

using HealthSystem;

using UnityEngine;

namespace Boosters
{
    public class BoosterSpawner : MonoBehaviour
    {
        [SerializeField] private Health _health;

        [SerializeField] private Transform _boosterHolder;

        [SerializeField] private float _coinsBoosterSpawnChance;

        [SerializeField] private float _medkitSpawnChance;

        private BoostersPool _boostersPool;

        [Inject]
        private void Construct(BoostersPool boostersPool)
        {
            _boostersPool = boostersPool;
        }

        private void OnEnable()
        {
            _health.Died += ActivateBooster;
        }

        private void OnDisable()
        {
            _health.Died -= ActivateBooster;
        }

        private void ActivateBooster()
        {
            float randomValue = Random.value;

            if (randomValue < _coinsBoosterSpawnChance)
            {
                _boostersPool.AddBooster(_boosterHolder.position, BoosterType.Coin);
            }
            else if (randomValue < _medkitSpawnChance)
            {
                _boostersPool.AddBooster(_boosterHolder.position, BoosterType.Medkit);
            }
            else return;

            enabled = false;
        }
    }
}