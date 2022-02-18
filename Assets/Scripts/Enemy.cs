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
            if(_score > LevelController.Current.score)
            {
                anim.SetBool("Idle", true);
                
            }else if(_score < LevelController.Current.score)
            {
                anim.SetBool("Hit", true);
                hitEffect.SetActive(true);
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
        yield return new WaitForSecondsRealtime(0.5f);

        rb.useGravity = true;
        
        gameObject.SetActive(false);



    }

    public void Still()
    {
        anim.SetBool("Idle", true);
    }
}
