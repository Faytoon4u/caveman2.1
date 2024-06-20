using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hiscoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI newHighScoreText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI totalGoldText;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button deleteScoreButton;

    private Player player;
    private Spawner spawner;

    private float score;
    private int goldScore;
    private int totalGoldScore;

    public float Score => score;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        LoadTotalGold();
        LoadHighScore();
        NewGame();
    }

    public void NewGame()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        score = 0f;
        goldScore = 0;
        gameSpeed = initialGameSpeed;
        enabled = true;
        player.NewGame();
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        newHighScoreText.gameObject.SetActive(false);
        //deleteScoreButton.gameObject.SetActive(false); // Hide delete score button during gameplay

        UpdateHiscore();
        UpdateScoreText();
        UpdateGoldText();
        UpdateTotalGoldText();
    }

    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;

        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        //deleteScoreButton.gameObject.SetActive(true); // Show delete score button during game over

        UpdateHiscore();
        SaveTotalGold();
    }

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        UpdateScoreText();
        player.ChangeSpeed(gameSpeed);

        // Check if score is 1000 or more to increase gravity
        if (score >= 1000 && Physics.gravity.y != -19.62f)
        {
            Physics.gravity = new Vector3(0, -19.62f, 0); // Set a stronger gravity
            Debug.Log("Gravity increased!");
        }
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScoreText();
    }

    public void AddGoldScore(int value)
    {
        goldScore += value;
        totalGoldScore += value;
        UpdateGoldText();
        UpdateTotalGoldText();
        SaveTotalGold();
        SaveGoldScore();
        CheckUnlockLevel();
    }

    private void UpdateScoreText()
    {
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
    }

    private void UpdateGoldText()
    {
        goldText.text = goldScore.ToString("D5");
    }

    private void UpdateTotalGoldText()
    {
        totalGoldText.text = totalGoldScore.ToString("D5");
    }

    private void UpdateHiscore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
            newHighScoreText.gameObject.SetActive(true);
        }

        hiscoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    }

    private void SaveTotalGold()
    {
        PlayerPrefs.SetInt("totalGoldScore", totalGoldScore);
        PlayerPrefs.Save();
    }

    private void SaveGoldScore()
    {
        PlayerPrefs.SetInt("goldScore", goldScore);
        PlayerPrefs.Save();
    }

    private void LoadTotalGold()
    {
        totalGoldScore = PlayerPrefs.GetInt("totalGoldScore", 0);
        UpdateTotalGoldText();
    }

    private void LoadHighScore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);
        hiscoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    }

    private void CheckUnlockLevel()
    {
        if (goldScore >= 6)
        {
            PlayerPrefs.SetInt("levelUnlocked", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene("dog");
        }
    }

    public void DeleteHighScore()
    {
        Debug.Log("DeleteHighScore button pressed"); // Debug log to confirm button press
        PlayerPrefs.DeleteKey("hiscore"); // Only delete the high score
        LoadHighScore(); // Update the high score text to reflect the deletion
    }
}
