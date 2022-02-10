using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Current;
    
    public float speed;
    private float _currentSpeed;
    public float thrust = 70f;
    public bool isGliding = false;
    public GameObject playButton;
    public float xSpeed;
    public bool gameActive = false;
    private float _lastTouchedX;
    public Camera cam;
    public float transitionSpeed;
    public Vector3 camoOffset;
    public Animator anim;
    public GameObject Wing;
    Rigidbody rb;

    void Start()
    {
        //_currentSpeed = speed;
        rb = GetComponent<Rigidbody>();

    }

   
    void Update()
    {
        if (gameActive)
        {


            Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed * Time.deltaTime);
            transform.position = newPos;

            if (isGliding)
            {
                 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + _currentSpeed * Time.deltaTime);
                transform.position = newPos;
            }

            if (isGliding && Input.GetMouseButton(0))
            {
                float xAxis = Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
                this.transform.Translate(xAxis, 0, Time.deltaTime);
               
            
            }else if(isGliding && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _lastTouchedX = Input.GetTouch(0).position.x;
            
            }else if (isGliding && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                float xAxis = (_lastTouchedX - Input.GetTouch(0).position.x) / Screen.width;
                _lastTouchedX = Input.GetTouch(0).position.x;
                this.transform.Translate(xAxis, 0, _currentSpeed * Time.deltaTime);
            }
            else
            {
                return;
            }

        }
        else
        {
            return;
        }
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

        if (other.CompareTag("up"))
        {
            rb.AddForce(0, thrust, 0, ForceMode.Impulse) ;
            rb.useGravity = true;
            StartCoroutine(turnOffGravity());
            rb.drag = 0.08f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("fall"))
        {
            anim.SetBool("fly", true);
            anim.SetBool("falling", false);

            StartCoroutine(wing());
          
        }
        
    }

   public void StartLevel()
    {
        gameActive = true;
        anim.SetBool("Run", true);
        playButton.SetActive(false);
    }

    IEnumerator turnOffGravity()
    {
        yield return new WaitForSecondsRealtime(2f);
        rb.useGravity = false;
    }

    IEnumerator wing()
    {
        yield return new WaitForSecondsRealtime(2f);
        Wing.SetActive(true);
        rb.useGravity = false;
        rb.drag = 0.2f;
        _currentSpeed = 50f;

        isGliding = true;

    }






}
