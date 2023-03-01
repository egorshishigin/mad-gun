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

            _time.text = "Time: " + gameTime;

            _kills.text = $"Kills: {kills}";

            _score.text = $"Score: {score}";
        }

        public void PanelFadeAnimation()
        {
            _viewPanel.DOFade(1, _fadeDuration).SetUpdate(true);
        }
    }
}