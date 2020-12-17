using System;
using System.Collections;
using System.Collections.Generic;
using RayFire;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class Brick : MonoBehaviour
{
    public int hitPoints = 1;
    
    public static event Action<Brick> OnBrickDestruction;
    private RayfireRigid _rayfireRigid;

    private void Start()
    {
        _rayfireRigid = gameObject.GetComponent<RayfireRigid>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Ball ball = other.gameObject.GetComponent<Ball>();
            TakeDamage(ball);
        }
    }

    private void TakeDamage(Ball ball)
    {
        hitPoints--;
        if (hitPoints <= 0)
        {
            //TODO: Destroy Effect
            
            OnBrickDestruction?.Invoke(this);
            _rayfireRigid.Demolish();
            ball.GetComponent<RayfireBomb>().Explode(0);
            foreach (var rayfireRigid in _rayfireRigid.fragments)
            {
                rayfireRigid.gameObject.GetComponent<MeshCollider>().enabled = false;
            }
            
        }
        else
        {
            //TODO: Damaged brick effect and graphics
        }
    }

    private void DestroyEffect()
    {
        
    }
}
