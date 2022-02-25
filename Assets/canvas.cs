using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class canvas : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = ("LEVEL " + (PlayerPrefs.GetInt("level") + 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
