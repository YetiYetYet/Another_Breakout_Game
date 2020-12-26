using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion
    
    public enum State
    {
        Play,
        Pause,
        GameOver,
        Victory
    }

    public int initialLives = 1;
    [ReadOnly]
    public int actualLives;
    [ReadOnly]
    public bool isGameStarted;
    [ReadOnly]
    public State gameState;

    public string levelName;
    public int score;

    public GameObject gameOverScreen;
    public GameObject victoryScreen;
    public GameObject pauseScreen;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Loading scene : " + scene.name);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("currentLevel")) PlayerPrefs.SetInt("currentLevel", 1);
        if(!PlayerPrefs.HasKey("score")) PlayerPrefs.SetInt("score", 0);
        Ball.OnBallDeath += OnBallDeath;
        actualLives = initialLives;
        gameState = State.Play;
    }

    private void OnBallDeath(Ball ball)
    {
        if (gameState == State.Victory) return;
        if (BallsManager.Instance.Balls.Count <= 0)
        {
            this.actualLives--;
            isGameStarted = false;
            if (actualLives < 1)
            {
                gameOverScreen.gameObject.SetActive(true);
                gameState = State.GameOver;
                CollectableManager.Instance.RemoveAllCollectables();
            }
            else
            {
                BallsManager.Instance.ResetBalls(true);
            }
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("currentLevel");
        PlayerPrefs.SetInt("currentLevel", currentLevel++);
        if (SceneUtility.GetBuildIndexByScenePath("Level_" + currentLevel) >= 0)
        {
            SceneManager.LoadScene("Level_" + currentLevel);
        }
        else
        {
            SceneManager.LoadScene("End");
        }
    }

    public void PauseLevel()
    {
        gameState = State.Pause;
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    public void ResumeLevel()
    {
        gameState = State.Play;
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

    public void Victory()
    {
        gameState = State.Victory;
        victoryScreen.SetActive(true);
        Time.timeScale = 0.8f;
        BallsManager.Instance.ResetBalls(false);
        CollectableManager.Instance.RemoveAllCollectables();
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnDisable()
    {
        Ball.OnBallDeath -= OnBallDeath;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
