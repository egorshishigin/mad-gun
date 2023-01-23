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

        [SerializeField] private float _droneSpawnChance;

        [SerializeField] private float _bulletTimeSpawnChance;

        private BoostersPool _boostersPool;

        private ActiveBoostersState _activeBoostersState;

        [Inject]
        private void Construct(BoostersPool boostersPool, ActiveBoostersState activeBoostersState)
        {
            _boostersPool = boostersPool;

            _activeBoostersState = activeBoostersState;
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
            else if (randomValue < _droneSpawnChance && !_activeBoostersState.Drone)
            {
                _boostersPool.AddBooster(_boosterHolder.position, BoosterType.Drone);
            }
            else if (randomValue < _bulletTimeSpawnChance && !_activeBoostersState.BulletTime)
            {
                _boostersPool.AddBooster(_boosterHolder.position, BoosterType.BulletTime);
            }
            else return;

            enabled = false;
        }
    }
}