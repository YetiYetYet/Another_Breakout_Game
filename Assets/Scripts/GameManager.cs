using System;
using System.Collections;
using System.Collections.Generic;
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
            return;
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion

    public int initialLives = 1;
    [ReadOnly]
    public int actualLives;
    [ReadOnly]
    public bool isGameStarted;
    [ReadOnly] 
    public bool gameOver;

    public int currentLevel;

    
    public GameObject _gameOverScreen;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Loading scene : " + scene.name);
        Initialisation();
    }

    // Start is called before the first frame update
    void Start()
    {
        Ball.OnBallDeath += OnBallDeath;
        actualLives = initialLives;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBallDeath(Ball ball)
    {
        if (BallsManager.Instance.Balls.Count <= 0)
        {
            this.actualLives--;
            isGameStarted = false;
            if (actualLives < 1)
            {
                _gameOverScreen.gameObject.SetActive(true);
                gameOver = true;
            }
            else
            {
                BallsManager.Instance.ResetBalls();
            }
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Initialisation();
    }

    public void NextLevel()
    {
        currentLevel++;
        SceneManager.LoadScene("Level_" + currentLevel);
    }

    private void Initialisation()
    {
        _gameOverScreen = GameObject.FindWithTag("Canvas/GameOver");
        _gameOverScreen.SetActive(false);
    }
    
    private void OnDisable()
    {
        Ball.OnBallDeath -= OnBallDeath;
    }
}
