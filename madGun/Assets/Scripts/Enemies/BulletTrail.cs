using System.Collections;
using System.Collections.Generic;

using Zenject;

using Player;

using UnityEngine;

namespace Enemies
{
    public class BulletTrail : MonoBehaviour
    {
        [SerializeField] private TrailRenderer _trail;

        [SerializeField] private int _damage;

        private BulletTrailPool _trailPool;

        private PlayerHitBox _player;

        [Inject]
        private void Construct(BulletTrailPool trailPool, PlayerHitBox player)
        {
            _trailPool = trailPool;

            _player = player;
        }

        public void ReturnToPool()
        {
            _trailPool.DeactivateTrail();
        }

        private void LaunchTrail(Vector3 startPosition, RaycastHit hit)
        {
            StartCoroutine(MoveTrail(startPosition, hit));
        }

        private IEnumerator MoveTrail(Vector3 startPosition, RaycastHit hit)
        {
            float time = 0f;

            Vector3 startPoint = _trail.transform.position;

            while (time < 1f)
            {
                _trail.transform.position = Vector3.Lerp(startPoint, hit.point, time);

                time += Time.deltaTime / _trail.time;

                yield return null;
            }

            _trail.transform.position = hit.point;

            StartCoroutine(DeactivateTrail());
        }

        private IEnumerator DeactivateTrail()
        {
            yield return new WaitForSeconds(_trail.time);

            _player.HitHandler(_damage);

            ReturnToPool();
        }

        public class Pool: MemoryPool<Vector3, RaycastHit, BulletTrail>
        {
            protected override void OnCreated(BulletTrail item)
            {
                item.gameObject.SetActive(false);
            }

            protected override void Reinitialize(Vector3 startPosition, RaycastHit hit, BulletTrail item)
            {
                item.transform.position = startPosition;

                item.gameObject.SetActive(true);

                item.LaunchTrail(startPosition, hit);
            }

            protected override void OnDespawned(BulletTrail item)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}