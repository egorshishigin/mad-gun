using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Boosters
{
    public class ActiveBoosterView : MonoBehaviour
    {
        [SerializeField] private ActiveBoosterBase _activeBooster;

        [SerializeField] private Image _boosterIcon;

        [SerializeField] private TMP_Text _boosterCount;

        private void OnEnable()
        {
            _activeBooster.Cooldowned += CooldownHandler;

            _activeBooster.BoosterUsed += UdpdateBoosterCount;

            _activeBooster.Refreshed += BoosterRefreshedHandler;
        }

        private void Start()
        {
            SetBoosterCountText(_activeBooster.Count);
        }

        private void OnDisable()
        {
            _activeBooster.Cooldowned -= CooldownHandler;

            _activeBooster.BoosterUsed -= UdpdateBoosterCount;

            _activeBooster.Refreshed -= BoosterRefreshedHandler;
        }

        private void SetBoosterCountText(int count)
        {
            _boosterCount.text = count.ToString();
        }

        private void CooldownHandler(float value)
        {
            _boosterIcon.fillAmount += 0.25f / value * Time.deltaTime;
        }

        private void UdpdateBoosterCount(int count)
        {
            _boosterIcon.fillAmount = 0f;

            _boosterCount.text = count.ToString();
        }

        private void BoosterRefreshedHandler()
        {
            _boosterIcon.fillAmount = 1f;
        }
    }
}