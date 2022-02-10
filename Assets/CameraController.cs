using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Current;

    public Transform target;
    public GameObject Player;
    public float smoothSpeed = 0.5f;
    public Vector3 offset;
    private Vector3 _offset;
    
    void Start()
    {
        _offset = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        transform.position = Player.transform.position;
    }

    public void CameraTransition()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        
    }
}
