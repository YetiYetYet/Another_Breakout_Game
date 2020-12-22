using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Paddle"))
        {
            Debug.Log("ApplyEffect");
            ApplyEffect();
        }

        if (other.CompareTag("Paddle") || other.CompareTag("DeathWall"))
        {
            Destroy(gameObject);
        }
    }

    protected abstract void ApplyEffect();
}
