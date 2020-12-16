using System;
using System.Collections.Generic;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
    #region Singleton
    
    private static BallsManager _instance;

    public static BallsManager Instance => _instance;

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
    public List<Ball> Balls { get; set; }
    public Vector3 ballSpawnerOffset;
    public Ball ballPrefab;
    public float initialBallSpeed = 250f;
    
    private Ball _initialBall;
    //private Rigidbody _initialBallRb;
    
    
    // Start is called before the first frame update
    void Start()
    {
        InitBall();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isGameStarted)
        {
            Vector3 paddlePosition = Paddle.Instance.gameObject.transform.position;
            Vector3 ballPosition = paddlePosition + ballSpawnerOffset;

            _initialBall.transform.position = ballPosition;

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                _initialBall.rb.isKinematic = false;
                _initialBall.rb.AddForce(new Vector3(0, initialBallSpeed, 0));
                GameManager.Instance.isGameStarted = true;
            }
        }
    }

    private void InitBall()
    {
        Vector3 paddlePos = Paddle.Instance.gameObject.transform.position;
        Vector3 startingPosition = paddlePos + ballSpawnerOffset;
        _initialBall = Instantiate(ballPrefab, startingPosition, Quaternion.identity);
        
        this.Balls = new List<Ball>{ _initialBall };
    }

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(GameObject.FindGameObjectWithTag("Paddle").transform.position + ballSpawnerOffset, 0.1f);
    }*/
}
