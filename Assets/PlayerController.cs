using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Current;
    
    public float speed;
    private float _currentSpeed;
    public float thrust = 70f;
    public bool isGliding = false;
    public GameObject playButton;
    public float xSpeed;
    public float limitX;
    public bool gameActive = false;
    private float _lastTouchedX;
    public Camera cam;
    public float transitionSpeed;
    public Vector3 camoOffset;
    public Animator anim;
    public GameObject Wing;
    public GameObject remy;
    public GameObject panel;
    Rigidbody rb;
    public GameObject wind;
    public float windSpeed;
    public bool windy = true;

    void Start()
    {
        //_currentSpeed = speed;
        rb = GetComponent<Rigidbody>();

    }

   
    void Update()
    {
        if (gameActive)
        {
            float newX = 0;
            float touchXDelta = 0;

            Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed * Time.deltaTime);
            transform.position = newPos;

            if (isGliding)
            {
                
                 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + _currentSpeed * Time.deltaTime);
                transform.position = newPos;
                

                if (windy)
                {
                    StartCoroutine(Windy());
                   
                    windy = false;
                }
            }

            if (isGliding && Input.GetMouseButton(0))
            {
                //float xAxis = Mathf.Clamp(xAxis, -limitX, limitX);
                //float xAxis = Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
                //this.transform.Translate(xAxis, 0, Time.deltaTime);
                touchXDelta = Input.GetAxis("Mouse X");
                newX = transform.position.x + xSpeed * touchXDelta * Time.deltaTime;
                newX = Mathf.Clamp(newX, -limitX, limitX);

                Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z +  Time.deltaTime);
                transform.position = newPosition;

            }
            else if(isGliding && Input.GetTouch(0).phase == TouchPhase.Began && Input.touchCount > 0)
            {
                touchXDelta = Input.GetTouch(0).position.x;
            
            }else if (isGliding && Input.GetTouch(0).phase == TouchPhase.Moved && Input.touchCount > 0)
            {
                //loat xAxis = (_lastTouchedX - Input.GetTouch(0).position.x) / Screen.width;
                //_lastTouchedX = Input.GetTouch(0).position.x;
                //this.transform.Translate(xAxis, 0, _currentSpeed * Time.deltaTime);

                touchXDelta = Input.GetTouch(0).deltaPosition.x / Screen.width;

                newX = transform.position.x + xSpeed * touchXDelta * Time.deltaTime;
                newX = Mathf.Clamp(newX, -limitX, limitX);

                Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z + _currentSpeed * Time.deltaTime);
                transform.position = newPosition;
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
           
           anim.SetBool("falling", true);
            speed = 5f;

            rb.useGravity = true;
        }

        /*if (other.CompareTag("fall"))
        {
            anim.SetBool("fidle", false);
            anim.SetBool("falling", true);
            speed = 5f;
        }*/

        if (other.CompareTag("up"))
        {
            
            rb.AddForce(0, thrust, 0, ForceMode.Impulse) ;
            
            rb.useGravity = false;
            StartCoroutine(turnOffGravity());
            rb.drag = 1f;
           transform.DOLocalRotate(new Vector3(0, 0, 360), 0.7f,RotateMode.LocalAxisAdd).SetEase(Ease.InOutQuad);
            Destroy(other.gameObject);
            
        }

        if (other.CompareTag("Finish"))
        {
            gameActive = false;
            panel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("trigger"))
        {
            anim.SetBool("fly", true);
            anim.SetBool("falling", false);

            StartCoroutine(wing());
          
        }
        if (other.CompareTag("wall"))
        {
            rb.useGravity = true;
        }
        
    }

   public void StartLevel()
    {
        gameActive = true;
        anim.SetBool("Run", true);
        playButton.SetActive(false);
    }

    public void Wind()
    {
        transform.GetChild(3).gameObject.SetActive(true);
        
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
        rb.drag = 0.5f;
        _currentSpeed = 50f;

        isGliding = true;

    }

    IEnumerator Windy()
    {
        wind.SetActive(true);
        
        
        yield return new WaitForSecondsRealtime(1f);
        //Wind();
        wind.SetActive(false);

        
        

        
    
    }






}
