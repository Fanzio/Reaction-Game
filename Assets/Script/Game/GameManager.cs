using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Tooltip("Durata della partita in secondi")]
    private float gameTimer;

    [Header("UI In-Game")]
    public GameObject inGamePanel; // Raggruppa i testi in-game (timer, score, reaction time)
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI enemyCountText;
    public TextMeshProUGUI reactionTimeText;

    [Header("UI Game Over")]
    public GameObject gameOverPanel; // Pannello che contiene la UI di Game Over (testi e bottoni)
    public TextMeshProUGUI gameOverEnemyCountText;
    public TextMeshProUGUI gameOverReactionTimeText;
    public TextMeshProUGUI gameOverAccuracyText;  // Nuovo campo per visualizzare la precisione

    private int enemyCount = 0;
    private float totalReactionTime = 0f;
    private int enemiesDestroyed = 0;

    // Nuove variabili per tracciare la precisione
    private int totalShots = 0;   // Tentativi totali
    private int hitCount = 0;     // Colpi a segno

    private bool isGameOver = false;
    public bool IsGameOver { get { return isGameOver; } }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        gameTimer = GameSettings.GameDuration;
        UpdateTimerUI();
        UpdateEnemyUI();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (!isGameOver)
        {
            if (gameTimer > 0)
            {
                gameTimer -= Time.deltaTime;
                UpdateTimerUI();
            }
            else
            {
                gameTimer = 0;
                UpdateTimerUI();
                GameOver();
            }
        }
    }

    void UpdateTimerUI()
    {
        timerText.text = "Time: " + Mathf.Ceil(gameTimer).ToString();
    }

    void UpdateEnemyUI()
    {
        enemyCountText.text = "Enemies: " + enemyCount.ToString();
        float avgReaction = (enemiesDestroyed > 0) ? totalReactionTime / enemiesDestroyed : 0f;
        reactionTimeText.text = "Your Reaction Time: " + avgReaction.ToString("F2") + "s";
    }

    // Metodo chiamato ogni volta che un nemico viene distrutto
    public void AddEnemyDestroyed(float reactionTime)
    {
        enemyCount++;
        enemiesDestroyed++;
        totalReactionTime += reactionTime;
        UpdateEnemyUI();
    }

    // Aggiungi i tentativi e i colpi a segno
    public void AddShot(bool hit)
    {
        totalShots++;
        if (hit)
        {
            hitCount++;
        }
    }

    // Calcola e mostra la precisione alla fine del gioco
    public void CalculateAccuracy()
    {
        if (totalShots > 0)
        {
            float accuracy = (float)hitCount / totalShots * 100f;
            gameOverAccuracyText.text = "Accuracy: " + accuracy.ToString("F2") + "%";
        }
        else
        {
            gameOverAccuracyText.text = "Accuracy: 0%";
        }
    }

    void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;

        if (inGamePanel != null)
            inGamePanel.SetActive(false);

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            gameOverEnemyCountText.text = enemyCountText.text;
            gameOverReactionTimeText.text = reactionTimeText.text;

            // Calcola la precisione alla fine del gioco
            CalculateAccuracy();
        }

        // Disabilita lo script di input per evitare ulteriori interazioni
        DestroyEnemy touchScript = FindObjectOfType<DestroyEnemy>();
        if (touchScript != null)
            touchScript.enabled = false;
    }

    // Metodo per il bottone "Quit": carica la scena con build index 0 (menu principale)
    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    // Metodo per il bottone "Restart": ricarica la scena corrente
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
