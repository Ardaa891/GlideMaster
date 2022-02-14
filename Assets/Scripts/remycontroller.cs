using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class remycontroller : MonoBehaviour
{
    Animator anim;
    
   
    
    void Start()
    {
        anim = GetComponent<Animator>();
       
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Run");

           
        

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "trigger")
        {
            anim.SetTrigger("Flip");
        }
    }
}
