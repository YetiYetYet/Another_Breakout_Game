using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField]
    private Vector3 targetPos;
    private Camera _camera;
    private Plane _plane;
    public float speed = 1;
    public float limit = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.position;
        _camera = Camera.main;
        _plane = new Plane(Vector3.forward, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (_plane.Raycast(ray, out float distance))
        {
            targetPos.x = Mathf.Clamp(ray.GetPoint(distance).x, -limit, limit);
            
        }
        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
    }
}
