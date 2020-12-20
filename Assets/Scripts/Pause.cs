using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            if (_gameManager.gameState == GameManager.State.Play)
            {
                _gameManager.PauseLevel();
            }
            else if (_gameManager.gameState == GameManager.State.Pause)
            {
                _gameManager.ResumeLevel();
            }
            
        }
    }
}
