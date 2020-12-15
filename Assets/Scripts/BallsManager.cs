using System.Collections.Generic;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
    public List<Ball> Balls { get; set; }
    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = new Vector3(10f, 10f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void initBall()
    {
        Vector3 startingPosition = new Vector3();
    }
}
