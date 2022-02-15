using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class cube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.DOLocalRotate(new Vector3(0, 0, 30), 0.5f, RotateMode.FastBeyond360).SetEase(Ease.OutFlash).SetLoops(2, LoopType.Yoyo);
    }
}
