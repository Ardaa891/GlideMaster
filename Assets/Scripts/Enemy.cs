using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject badGuy;
    Animator anim;
    Rigidbody rb;

    
    void Start()
    {
        anim = badGuy.GetComponent<Animator>();
        rb = badGuy.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("Hit");
            StartCoroutine(Die());
        }
    }


    void Update()
    {
        
    }

    IEnumerator Die()
    {
        yield return new WaitForSecondsRealtime(1f);

        rb.useGravity = true;
        anim.SetBool("Fall", true);
        badGuy.gameObject.SetActive(false);



    }
}
