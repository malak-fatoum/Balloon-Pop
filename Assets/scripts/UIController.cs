using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Everything the player sees on the Canvas: score, timer, and game over panel.
public class UIController : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text finalScoreText;
    [SerializeField] TMP_Text bestScoreText;
    [SerializeField] GameObject gameOverPanel;

    int score;
    int bestScore;
    float timeLeft = 60f;
    bool gameStarted;

    void Awake()
    {
        gameOverPanel.SetActive(false);

        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    void Update()
    {
        if (!gameStarted)
            return;

        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft < 0)
                timeLeft = 0;

            timerText.text = Mathf.CeilToInt(timeLeft).ToString();

            if (timeLeft <= 0)
            {
                GameManager.instance.TimeUp();
            }
        }
    }

    public void StartGame()
    {
        score = 0;
        timeLeft = 60f;
        gameStarted = true;

        scoreText.text = "0";
        timerText.text = "60";

        gameOverPanel.SetActive(false);
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }

    public void ShowGameOver()
    {
        gameStarted = false;

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
        }

        finalScoreText.text = score.ToString();
        bestScoreText.text = bestScore.ToString();

        gameOverPanel.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}