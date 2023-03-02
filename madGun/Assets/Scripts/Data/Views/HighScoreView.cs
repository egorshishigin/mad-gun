using Zenject;

using TMPro;

using Data;

using UnityEngine;

public class HighScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _highScore;

    private GameDataIO _gameData;

    [Inject]
    private void Construct(GameDataIO gameData)
    {
        _gameData = gameData;
    }

    private void Awake()
    {
        SetHighScoreText();
    }

    private void SetHighScoreText()
    {
        _highScore.text = _gameData.GameData.HighScore.ToString();
    }
}
