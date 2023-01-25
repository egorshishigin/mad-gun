using UnityEngine;

namespace HealthSystem
{
    public class EnemyRagdoll : MonoBehaviour
    {
        [SerializeField] private Health _health;

        [SerializeField] private Rigidbody[] _bodyParts;

        [SerializeField] private float _pushBackForce;

        private void OnEnable()
        {
            _health.Died += DiedHandler;
        }

        private void OnDisable()
        {
            _health.Died -= DiedHandler;
        }

        private void Awake()
        {
            foreach (var item in _bodyParts)
            {
                item.isKinematic = true;
            }
        }

        private void DiedHandler()
        {
            ActivateRagdoll();

            _health.enabled = false;

            enabled = false;
        }

        private void ActivateRagdoll()
        {
            foreach (var item in _bodyParts)
            {
                item.isKinematic = false;

                item.AddForce(Vector3.forward * _pushBackForce, ForceMode.Impulse);
            }
        }
    }
}