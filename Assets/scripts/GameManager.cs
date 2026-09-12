using UnityEngine;

// The boss. Starts the game, reacts to a pop, counts misses, and ends the game.
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] SoundControoller soundControoller;
    [SerializeField] UIController uiController;
    [SerializeField] BalloonSpawner balloonSpanwer;

    [SerializeField] int maxMisses = 5;

    [SerializeField] GameObject startScreen;
    [SerializeField] GameObject gameplayUI;

    int misses;
    bool gameOver;
    float gameTime;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        startScreen.SetActive(true);
        gameplayUI.SetActive(false);
    }

    void Update()
    {
        if (gameOver)
        {
            return;
        }

        if (gameplayUI.activeSelf)
        {
            gameTime += Time.deltaTime;
        }
    }

    public void StartGame()
    {
        startScreen.SetActive(false);
        gameplayUI.SetActive(true);

        gameTime = 0f;
        misses = 0;
        gameOver = false;

        uiController.StartGame();
        balloonSpanwer.Initialize();
    }

    // A balloon was popped by the player.
    public void DestroyBalloon()
    {
        if (gameOver)
        {
            return;
        }

        soundControoller.PlayDestroyBalloonEffect();
        uiController.AddScore(1);
    }

    // A balloon floated off the top without being popped.
    public void BalloonEscaped()
    {
        if (gameOver)
        {
            return;
        }

        misses++;

        if (misses >= maxMisses)
        {
            GameOver();
        }
    }

    public void TimeUp()
    {
        if (gameOver)
        {
            return;
        }

        GameOver();
    }

    public float GetBalloonSpeedMultiplier()
    {
        if (gameTime < 10f)
            return 1f;

        if (gameTime < 20f)
            return 1.2f;

        if (gameTime < 30f)
            return 1.4f;

        if (gameTime < 40f)
            return 1.7f;

        if (gameTime < 50f)
            return 2f;

        return 2.4f;
    }
    void GameOver()
    {
        gameOver = true;

        balloonSpanwer.StopSpawning();

        GameObject[] balloons = GameObject.FindGameObjectsWithTag("Balloon");

        foreach (GameObject balloon in balloons)
        {
            Destroy(balloon);
        }

        uiController.ShowGameOver();
    }
    public bool IsGameOver()
    {
        return gameOver;
    }
}