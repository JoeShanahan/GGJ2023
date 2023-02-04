using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Worker : MonoBehaviour
{
    private Coroutine workRoutine;
    
    private Resource _resourceType;
    
    private Transform _resourceGatherPoint;

    private Transform _resourceDropOfPoint;
    
    [SerializeField]
    private float _gatheringDuration;
    
    [SerializeField]
    private float _walkSpeed;

    public void Initialize(Transform resourceGatherPoint, Transform resourceDropOfPoint, Resource resourceType)
    {
        SwitchResourceGatherRoutine(resourceGatherPoint, resourceDropOfPoint, resourceType);
    }

    public void SwitchResourceGatherRoutine(Transform resourceGatherPoint, Transform resourceDropOfPoint, Resource resourceType)
    {
        if (workRoutine != null)
        {
            StopCoroutine(workRoutine);
        }
        
        _resourceGatherPoint = resourceGatherPoint;
        _resourceDropOfPoint = resourceDropOfPoint;
        _resourceType = resourceType;
        workRoutine = StartCoroutine(WorkerRoutine());
    }
    
    
    public IEnumerator WorkerRoutine()
    {
        while (true)
        {
            var idleDuration = Random.Range(0f, 3f);
            yield return new WaitForSeconds(idleDuration);
            
            // walk to resource
            var distance = (transform.position - _resourceGatherPoint.position).magnitude;
            var duration = distance / _walkSpeed;
            transform.DOMove(_resourceGatherPoint.position, duration);
            yield return new WaitForSeconds(duration);
            
            // gather resource
            yield return new WaitForSeconds(_gatheringDuration);
        
            // walk to tree
            distance = (transform.position - _resourceDropOfPoint.position).magnitude;
            duration = distance / _walkSpeed;
            transform.DOMove(_resourceDropOfPoint.position, duration);
            yield return new WaitForSeconds(duration);
            
            //drop of and increase resource
            ResourceSystem.Instance.IncreaseResource(_resourceType, 1);
        }
    }
}
