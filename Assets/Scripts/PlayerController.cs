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
    public bool isFinished = false;
    public bool gameActive = false;
    public bool firstCol = false;
    
    
    
    public Animator anim;
    public GameObject Wing;
    public GameObject diamondWing;
    public GameObject goldenWing;
    public GameObject remy;
    public GameObject panel;
    Rigidbody rb;
   
    public bool windy = true;
    public GameObject plane;
    [SerializeField]
    public GameObject finishEnemy;
    public GameObject charUI;
    public GameObject diamondEffect;
    public GameObject goldenEffect;
    public GameObject scoreImage;
    public Animator sizeAnim;
    public GameObject finishStar;
    public GameObject World;
    public GameObject Collectables;
    public GameObject Finish;
    public GameObject backpack;
    public Animator uiAnim;
    Sequence seq;
    public GameObject levelFailedMenu;
    
    

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //_currentSpeed = speed;
        rb = GetComponent<Rigidbody>();
        Application.targetFrameRate = 60;
        DOTween.Init();
        Current = this;
        sizeAnim = scoreImage.GetComponent<Animator>();
        uiAnim = charUI.GetComponent<Animator>();
    }

   
    void Update()
    {
        seq = DOTween.Sequence();
        if (gameActive)
        {
            float newX = 0;
            float touchXDelta = 0;
           

            Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 20 * Time.deltaTime);
            transform.position = newPos;

            if (gameActive && isFinished)
            {

                _currentSpeed = 35;
                uiAnim.SetBool("descale", true);
                float desiredYPos = (finishEnemy.transform.position.y + 5);
                float desiredXPos = (finishEnemy.transform.position.x);

                transform.DOMoveY(desiredYPos, 7f);
                transform.DOMoveX(desiredXPos, 1f);

                





            }

            if (isGliding)
            {
                
                 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + _currentSpeed * Time.deltaTime);
                transform.position = newPos;
                

               
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

                if (touchXDelta >= -limitX && touchXDelta < 0)
                {
                    transform.DORotate(new Vector3(0, 0, 30), 0.2f, RotateMode.Fast).SetEase(Ease.Linear).OnComplete(()=>Rot1());




                }
                else if(touchXDelta <= limitX && touchXDelta > 0)
                {
                    transform.DORotate(new Vector3(0, 0, -30), 0.2f, RotateMode.Fast).SetEase(Ease.Linear).OnComplete(() => Rot());
                }
                
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

                Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z + 50f * Time.deltaTime);
                transform.position = newPosition;

                if (touchXDelta >= -limitX && touchXDelta < 0)
                {
                    transform.DORotate(new Vector3(0, 0, 30), 0.2f, RotateMode.Fast).SetEase(Ease.Linear).OnComplete(() => Rot1());




                }
                else if (touchXDelta <= limitX && touchXDelta > 0)
                {
                    transform.DORotate(new Vector3(0, 0, -30), 0.2f, RotateMode.Fast).SetEase(Ease.Linear).OnComplete(() => Rot());
                }

                


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

        if (firstCol)
        {
            if(LevelController.Current.score <= 0)
            {
                seq.Append(transform.DOMoveY(-100, 5f));
                seq.Join(transform.DOLocalRotate(new Vector3(0, 0, 3600), 5f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental));
                StartCoroutine(Die());
                CameraController.Current.target = null;
                uiAnim.SetBool("descale", true);
                levelFailedMenu.SetActive(true);
            }
            else
            {
                return;
            }

            
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("wall"))
        {
            anim.SetBool("Flip", true);
            anim.SetBool("Run", false);
            anim.SetBool("Idle", false);
           
        }

        if (other.CompareTag("trigger"))
        {
           anim.SetBool("Flip", false);

            //anim.SetBool("falling", true);
            anim.SetBool("fly", true);
            anim.SetBool("Idle", false);
            speed = 0f;
            StartCoroutine(wing());
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
            firstCol = true;
            float worldYpos = (World.transform.position.y);
            float colYpos = (Collectables.transform.position.y);
            float charYpos = (gameObject.transform.position.y);
            float finishYpos = (Finish.transform.position.y);
            
            transform.DOMoveY(charYpos + 5f, 0.5f).SetEase(Ease.Linear);
            Collectables.transform.DOMoveY(colYpos + 5f, 0.5f).SetEase(Ease.Linear);
            Finish.transform.DOMoveY(finishYpos - 1 , 0.5f).SetEase(Ease.Linear);

            //rb.AddForce(0, thrust, 0, ForceMode.Impulse) ;
            World.transform.DOMoveY(worldYpos - 7, 0.5f).SetEase(Ease.OutCirc);
            rb.useGravity = false;
            StartCoroutine(turnOffGravity());
            rb.drag = 1f;
           transform.DOLocalRotate(new Vector3(0, 0, 360), 0.5f,RotateMode.LocalAxisAdd).SetEase(Ease.InOutQuad);
            LevelController.Current.ChangeScore(2);
            Destroy(other.gameObject);
            
        }

        if (other.CompareTag("down"))
        {
            firstCol = true;
            float worldYpos = (World.transform.position.y);
            float finishYpos = (Finish.transform.position.y);
            float colYpos = (Collectables.transform.position.y);
            float charYpos = (gameObject.transform.position.y);

            transform.DOMoveY(charYpos - 5, 0.5f).SetEase(Ease.Linear);
            Collectables.transform.DOMoveY(colYpos - 4.5f, 0.5f).SetEase(Ease.Linear);
            Finish.transform.DOMoveY(finishYpos + 1, 0.5f).SetEase(Ease.Linear);

            World.transform.DOMoveY(worldYpos + 7, 0.5f).SetEase(Ease.OutCirc);
            //rb.AddForce(0, -thrust, 0, ForceMode.Impulse);
            rb.useGravity = false;
            StartCoroutine(turnOffGravity());
            rb.drag = 1f;
            transform.DOLocalRotate(new Vector3(0, 0, 360), 0.5f, RotateMode.LocalAxisAdd).SetEase(Ease.InOutQuad);
            LevelController.Current.ChangeScore(-2);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Finish"))
        {
            isFinished = true;
            //gameActive = false;
            //panel.SetActive(true);
        }

        if (other.CompareTag("Enemy"))
        {
            float enemyScore = other.GetComponent<Enemy>()._score;

            if(LevelController.Current.score < enemyScore)
            {
                seq.Append(transform.DOMoveY(-100, 5f));
                seq.Join(transform.DOLocalRotate(new Vector3(0, 0, 3600), 5f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental)) ;
                StartCoroutine(Die());
                CameraController.Current.target = null;
                uiAnim.SetBool("descale", true);
                //gameActive = false;
                levelFailedMenu.SetActive(true);



            }
            else if (LevelController.Current.score > enemyScore)
            {
                LevelController.Current.FightScore(10);

                anim.SetTrigger("Kick");
            }
       
        }

        if (other.CompareTag("FinishEnemy"))
        {
            float enemyScore = other.GetComponent<Enemy>()._score;

            if (LevelController.Current.score < enemyScore)
            {
                gameActive = false;
                panel.SetActive(true);
                finishStar.SetActive(true);
                anim.SetBool("Idle", true);
                anim.SetBool("fly", false);

                Enemy.Current.Still();

            }
            else if (LevelController.Current.score > enemyScore)
            {
                

                anim.SetTrigger("Kick");
            }
        }

        if (other.CompareTag("Endlevel"))
        {
            gameActive = false;
            panel.SetActive(true);
            finishEnemy.SetActive(true);
        }

        if (firstCol && other.CompareTag("down"))
        {
            seq.Append(transform.DOMoveY(-100, 5f));
            seq.Join(transform.DOLocalRotate(new Vector3(0, 0, 3600), 5f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental));
            StartCoroutine(Die());
            CameraController.Current.target = null;
            uiAnim.SetBool("descale", true);
            //gameActive = false;
            levelFailedMenu.SetActive(true);
        }

      
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("trigger"))
        {
            //anim.SetBool("fly", true);
            //anim.SetBool("falling", false);
            

            //anim.SetBool("falling", true);
            
            //StartCoroutine(wing());

        }
        if (other.CompareTag("wall"))
        {
            rb.useGravity = true;
            plane.SetActive(false);

        }

        if (other.CompareTag("Enemy"))
        {
            _currentSpeed = 65;
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _currentSpeed = 50;
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
        yield return new WaitForSecondsRealtime(0.1f);
        Wing.SetActive(true);
        rb.useGravity = false;
        rb.drag = 0.22f;
        _currentSpeed = 65f;
        charUI.SetActive(true);
        isGliding = true;

    }

    IEnumerator Die()
    {
        yield return new WaitForSecondsRealtime(5f);
        gameObject.SetActive(false);
    }

    

    void Rot()
    {
        transform.DOLocalRotate(new Vector3(0, 0, -1), 0.2f, RotateMode.Fast).SetEase(Ease.Linear);
    }

    void Rot1()
    {
        transform.DOLocalRotate(new Vector3(0, 0, 0), 0.2f, RotateMode.Fast).SetEase(Ease.Linear);
    }






}
