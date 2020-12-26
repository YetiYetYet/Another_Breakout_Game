using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Paddle : MonoBehaviour
{
    #region Singleton
    
    private static Paddle _instance;

    public static Paddle Instance => _instance;

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
            //DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion
    
    private Rigidbody _rb;
    [SerializeField]
    private Vector3 targetPos;
    private Camera _camera;
    private Plane _plane;
    public float speed = 1;
    public float limit = 3;
    public float ballAngleMultiplicator;
    [ShowInInspector, ReadOnly] 
    private Vector3 _initialScaling;
    [ShowInInspector, ReadOnly] 
    private Vector3 _targetScaling;
    

    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.position;
        _initialScaling = transform.localScale;
        _targetScaling = _initialScaling;
        _camera = Camera.main;
        _plane = new Plane(Vector3.forward, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.gameState == GameManager.State.Victory || GameManager.Instance.gameState == GameManager.State.GameOver)
            return;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (_plane.Raycast(ray, out float distance))
        {
            targetPos.x = Mathf.Clamp(ray.GetPoint(distance).x, -limit, limit);
            
        }
        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime * 10);
        
        if (_targetScaling != transform.localScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _targetScaling, Time.deltaTime * 10); 
        }
    }

    public void ChangeScale(Vector3 targetScaling)
    {
        _targetScaling = targetScaling;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Ball ball = other.gameObject.GetComponent<Ball>();
            Vector3 hitPoint = other.contacts[0].point;
            //Vector3 paddleCenter = 

            ball.rb.velocity = Vector3.zero;
            var position = transform.position;
            float diff = position.x - hitPoint.x;

            ball.rb.AddForce(hitPoint.x < position.x
                ? new Vector3(-Mathf.Abs(diff * ballAngleMultiplicator), BallsManager.Instance.initialBallSpeed)
                : new Vector3(Mathf.Abs(diff * ballAngleMultiplicator), BallsManager.Instance.initialBallSpeed));
        }
    }
}
