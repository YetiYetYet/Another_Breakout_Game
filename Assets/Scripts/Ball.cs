using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody rb;
    public int ballDamage = 1;
    public static event Action<Ball> OnBallDeath;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void Die()
    {
        OnBallDeath?.Invoke(this);
        Destroy(gameObject);
    }
}
