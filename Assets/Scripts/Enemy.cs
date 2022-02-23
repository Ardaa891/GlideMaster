using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public static Enemy Current;
    public GameObject badGuy;
    public Animator anim;
    Rigidbody rb;
    public TextMeshPro enemyScore;
    public float _score;
    public GameObject hitEffect;
    public GameObject confetti;
    
    void Start()
    {
        Current = this;
        anim = badGuy.GetComponent<Animator>();
        rb = badGuy.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(_score > PlayerController.Current.score)
            {
                anim.SetBool("Idle", true);
                
            }else if(_score < PlayerController.Current.score)
            {
                anim.SetBool("Hit", true);
                StartCoroutine(Hit());
                //hitEffect.SetActive(true);
                confetti.SetActive(true);
                StartCoroutine(Die());
                
            }
           // anim.SetBool("Hit",true);
            
        }
    }


    void Update()
    {
        
    }

    IEnumerator Die()
    {
        yield return new WaitForSecondsRealtime(0.7f);

        rb.useGravity = true;
        
        gameObject.SetActive(false);

        

    }
    
    IEnumerator Hit()
    {
        yield return new WaitForSecondsRealtime(0.07f);

        hitEffect.SetActive(true);
    }

    

    public void Still()
    {
        anim.SetBool("Idle", true);
    }
}
