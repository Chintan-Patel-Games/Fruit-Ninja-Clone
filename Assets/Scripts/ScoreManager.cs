using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton instance for global access
    private int score; // Player's score
    private int lives = 3; // Player's lives
    private bool gameOver = false;

    [SerializeField] private TextMeshProUGUI scoreText; // UI Text element to display score
    [SerializeField] private TextMeshProUGUI livesText; // UI Text element to display lives
    [SerializeField] private TextMeshProUGUI gameOverText; // UI Text element to display lives

    private void Awake()
    {
        // Ensure there is only one instance of ScoreManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep score manager alive between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        score = 0; // Initialize score
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount; // Increase score
        UpdateScoreUI();
    }

    public void SubScore(int amount)
    {
        score -= amount; // Decrease score
        UpdateScoreUI();
    }

    public void SubLives(int amount)
    {
        lives -= amount; // Decrease score
        if (lives <= 0) { gameOver = true; }
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score; // Update the score text in the UI
        }

        if (livesText != null)
        {
            livesText.text = "Lives: " + lives; // Update the score text in the UI
        }

        if (gameOver)
        {
            gameOverText.enabled  = true;
            Time.timeScale = 0;
        }
    }
}