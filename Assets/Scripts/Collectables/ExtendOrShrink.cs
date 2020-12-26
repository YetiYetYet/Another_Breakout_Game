using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class ExtendOrShrink : Collectable
{
    public float scalingValue = 1;

    public float maxScaling;
    public float minScaling;

    public bool isShrink;
    
    [ReadOnly]
    private Vector3 _targetScaling;

    private Paddle _paddle;

    public void Start()
    {
        _paddle = Paddle.Instance;

    }

    public void Update()
    {
        
    }

    protected override void ApplyEffect()
    {
        Vector3 initialScaling = _paddle.transform.localScale;
        if (isShrink)
        {
            if (initialScaling.z <= minScaling) return;
            _targetScaling = new Vector3(initialScaling.x, initialScaling.y, _paddle.transform.localScale.z - scalingValue);
            _paddle.limit += scalingValue/2;
        }
        else
        {
            if (initialScaling.z >= maxScaling) return;
            _targetScaling = new Vector3(initialScaling.x, initialScaling.y, _paddle.transform.localScale.z + scalingValue);
            _paddle.limit -= scalingValue/2;
        }
        _paddle.ChangeScale(_targetScaling);
    }
}
