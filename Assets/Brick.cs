using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class Brick : MonoBehaviour
{
    public int hitPoints = 1;
    
    public static event Action<Brick> OnBrickDestruction;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Ball ball = other.gameObject.GetComponent<Ball>();
            takeDamage(ball);
        }
    }

    private void takeDamage(Ball ball)
    {
        hitPoints--;
        if (hitPoints <= 0)
        {
            //TODO: Destroy Effect
            
            OnBrickDestruction?.Invoke(this);
            Destroy(gameObject);
        }
        else
        {
            //TODO: Damaged brick effect and graphics
        }
    }
}
