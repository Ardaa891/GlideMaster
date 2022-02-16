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
            }
           // anim.SetBool("Hit",true);
            
        }
    }


    void Update()
    {
        
    }

    IEnumerator Die()
    {
        yield return new WaitForSecondsRealtime(1f);

        rb.useGravity = true;
        
        badGuy.gameObject.SetActive(false);



    }

    public void Still()
    {
        anim.SetBool("Idle", true);
    }
}
