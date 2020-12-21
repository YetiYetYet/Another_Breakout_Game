using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score : \n" + PlayerPrefs.GetInt("score").ToString("D5");
        livesText.text = "Lives : " + GameManager.Instance.initialLives;
        Brick.OnBrickDestruction += OnBrickDestruction;
        Ball.OnBallDeath += OnBallDeath;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBrickDestruction(Brick brick)
    {
        AddScore(brick.scoreValue);
    }

    public void AddScore(int score)
    {
        int currentScore = PlayerPrefs.GetInt("score");
        currentScore += score;
        PlayerPrefs.SetInt("score", currentScore);
        scoreText.text = "Score : \n" + PlayerPrefs.GetInt("score").ToString("D5");
    }
    
    private void OnBallDeath(Ball ball)
    {
        livesText.text = "Lives : " + GameManager.Instance.actualLives;
    }

    private void OnDisable()
    {
        Brick.OnBrickDestruction -= OnBrickDestruction;
        Ball.OnBallDeath -= OnBallDeath;
    }
}
