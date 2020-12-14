using UnityEngine;

public class Ball : MonoBehaviour
{
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
}
