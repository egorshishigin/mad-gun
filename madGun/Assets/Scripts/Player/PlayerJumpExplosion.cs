using System.Collections;
using System.Collections.Generic;

using Zenject;

using UnityEngine;
using Weapons;

namespace Player
{
    public class PlayerJumpExplosion : MonoBehaviour
    {
        [SerializeField] private Explosion _explosion;

        [SerializeField] private ParticleSystem _particleSystem;

        [SerializeField] private AudioSource _audio;

        private PlayerJumpExplosionPool _pool;

        [Inject]
        private void Construct(PlayerJumpExplosionPool pool)
        {
            _pool = pool;
        }

        public void ReturnToPool()
        {
            _pool.RemoveExplosion();
        }

        private void Explode()
        {
            _particleSystem.Play();

            _audio.Play();

            _explosion.ExplosionDamage();
        }

        public class Pool: MonoMemoryPool<Vector3, PlayerJumpExplosion>
        {
            protected override void OnCreated(PlayerJumpExplosion item)
            {
                item.gameObject.SetActive(false);
            }

            protected override void Reinitialize(Vector3 startPosition, PlayerJumpExplosion item)
            {
                item.transform.position = startPosition;

                item.gameObject.SetActive(true);

                item.Explode();
            }

            protected override void OnDespawned(PlayerJumpExplosion item)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}