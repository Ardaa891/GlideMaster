using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private float _currentSpeed;
    public float limitX;
    public bool isGliding = false;
    float newX = 0;
    float touchXdelta = 0;
    public float xSpeed;

    public Animator anim;
    Rigidbody rb;

    void Start()
    {
        _currentSpeed = speed;
        rb = GetComponent<Rigidbody>();

    }

   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("Run", true);
        }

        

        if (isGliding)
        {

            

            if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                touchXdelta = Input.GetTouch(0).deltaPosition.x / Screen.width;
            }else if (Input.GetMouseButton(0))
            {
                touchXdelta = Input.GetAxis("Mouse X");
                anim.SetBool("Run", false);
            }

           

            
        }
        newX = transform.position.x + xSpeed * Time.deltaTime * touchXdelta;
        newX = Mathf.Clamp(newX, -limitX, limitX);
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + _currentSpeed * Time.deltaTime);
        transform.position = newPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("wall"))
        {
            anim.SetBool("Flip", true);
            anim.SetBool("Run", false);
        }

        if (other.CompareTag("trigger"))
        {
            anim.SetBool("Flip", false);
            anim.SetBool("fidle", true);

            rb.useGravity = true;
        }

        if (other.CompareTag("fall"))
        {
            anim.SetBool("fidle", false);
            anim.SetBool("falling", true);
            speed = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("fall"))
        {
            anim.SetBool("fly", true);
            anim.SetBool("falling", false);

            rb.useGravity = false;
            rb.drag = 0.1f;
            speed = 5000f;

            isGliding = true;
        }
        
    }





}
