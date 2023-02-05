using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TweenScale : MonoBehaviour
{
    private Vector3 start;
    
    [SerializeField]
    private float loopDuration;

    [SerializeField]
    private Vector3 target;

    public void Start()
    {
        start = transform.localScale;
    }

    public void OnEnable()
    {
        StartCoroutine(ScaleRoutine());
    }

    public IEnumerator ScaleRoutine()
    {
 
        
        while (true)
        {
            transform.DOScale(target, loopDuration);
            yield return new WaitForSeconds(loopDuration);
            transform.DOScale(start, loopDuration);
            yield return new WaitForSeconds(loopDuration);
        }
    }
}