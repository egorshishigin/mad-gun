using TMPro;

using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

namespace GameOver
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coins;

        [SerializeField] private TMP_Text _time;

        [SerializeField] private TMP_Text _kills;

        [SerializeField] private TMP_Text _score;

        [SerializeField] private CanvasGroup _viewPanel;

        [SerializeField] private float _fadeDuration;

        public void SetGameOverValues(int coins, float time, int kills, int score)
        {
            _coins.text = coins.ToString();

            float minutes = Mathf.FloorToInt(time / 60);

            float seconds = Mathf.FloorToInt(time % 60);

            string gameTime = $"{minutes:00}:{seconds:00}";

            _time.text = gameTime;

            _kills.text = kills.ToString();

            _score.text = score.ToString();
        }

        public void SetDoubleCoinsText(int value)
        {
            _coins.text += $" + {value}";
        }

        public void PanelFadeAnimation()
        {
            _viewPanel.DOFade(1, _fadeDuration).SetUpdate(true);
        }

        public void PanelFadeOutAnimation()
        {
            Tween tween = _viewPanel.DOFade(0, _fadeDuration).SetUpdate(true);

            tween.OnComplete(() =>
            {
                gameObject.SetActive(false);

                tween.Kill();
            });
        }
    }
}