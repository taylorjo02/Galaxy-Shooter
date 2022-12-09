using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Sprite[] _livesSprite;

    [SerializeField]
    private Image _livesImg;

    [SerializeField]
    private Text _gameOverText;

    [SerializeField]
    private Text _restartGame;

    [SerializeField]
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _restartGame.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("GameManager is NULL.");
        }
    }

    public void UpdateScoreUI(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _livesSprite[currentLives];

        if (currentLives == 0)
        {
            GameOverSequence();
        }

    void GameOverSequence()
        {
            _gameManager.GameOver();
            _gameOverText.gameObject.SetActive(true);
            _restartGame.gameObject.SetActive(true);
            StartCoroutine(GameOverFlickerRoutine());
        }

    IEnumerator GameOverFlickerRoutine()
        {
            while (true)
            {
                _gameOverText.text = "GAME OVER";
                yield return new WaitForSeconds(0.5f);
                _gameOverText.text = "";
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
