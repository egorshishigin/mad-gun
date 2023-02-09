using UnityEngine;

namespace HealthSystem
{
    public class EnemyRagdoll : MonoBehaviour
    {
        [SerializeField] private Health _health;

        [SerializeField] private Rigidbody[] _bodyParts;

        [SerializeField] private float _pushBackForce;

        [SerializeField] private ParticleSystem _fleshEffect;

        [SerializeField] private GameObject _model;

        [SerializeField] private GameObject _bones;

        [SerializeField] private AudioSource _fleshSound;

        private void OnEnable()
        {
            _health.Died += DiedHandler;

            _health.GoreDied += GoreDiedHandler;
        }

        private void Start()
        {
            _fleshSound = _fleshEffect.GetComponent<AudioSource>();
        }

        private void GoreDiedHandler()
        {
            _model.SetActive(false);

            _bones.SetActive(false);

            _fleshEffect.Play();

            _fleshSound.PlayOneShot(_fleshSound.clip);
        }

        private void OnDisable()
        {
            _health.Died -= DiedHandler;

            _health.GoreDied -= GoreDiedHandler;
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

                item.AddForce(-item.transform.forward * _pushBackForce, ForceMode.Impulse);
            }
        }
    }
}