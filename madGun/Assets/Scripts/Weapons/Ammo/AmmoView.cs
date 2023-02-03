using Zenject;

using TMPro;

using UnityEngine;

namespace Weapons
{
    public class AmmoView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _ammoText;

        private Ammo _ammo;

        [Inject]
        private void Construct(Ammo ammo)
        {
            _ammo = ammo;
        }

        private void OnEnable()
        {
            _ammo.AmmoCountChanged += UpdateAmmoText;
        }

        private void OnDisable()
        {
            _ammo.AmmoCountChanged -= UpdateAmmoText;
        }

        public void UpdateAmmoText(int value)
        {
            _ammoText.text = value.ToString();
        }

        public void UpdateAmmoByID(int id)
        {
            _ammoText.text = _ammo.AmmoSupply[id].ToString();
        }
    }
}