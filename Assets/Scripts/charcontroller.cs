using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charcontroller : MonoBehaviour
{
    public float Speed;
    private float _currentSpeed;

    
    void Start()
    {
        _currentSpeed = Speed;
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + _currentSpeed * Time.deltaTime);
            transform.position = newPos;
        }
    }
}
