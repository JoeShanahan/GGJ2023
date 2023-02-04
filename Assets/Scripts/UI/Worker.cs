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

    [SerializeField]
    private float _jumpPower = 1;

    [SerializeField]
    private float _jumpsPerSecond = 2;

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
            int numJumps = Mathf.RoundToInt(duration * _jumpsPerSecond);
            transform.DOJump(_resourceGatherPoint.position + Vector3.back, _jumpPower, numJumps, duration).SetEase(Ease.Linear);
            yield return new WaitForSeconds(duration);
            
            // gather resource
            yield return new WaitForSeconds(_gatheringDuration);
        
            // walk to tree
            distance = (transform.position - _resourceDropOfPoint.position).magnitude;
            duration = distance / _walkSpeed;
            numJumps = Mathf.RoundToInt(duration * _jumpsPerSecond);
            transform.DOJump(_resourceDropOfPoint.position + Vector3.back, _jumpPower, numJumps, duration).SetEase(Ease.Linear);
            yield return new WaitForSeconds(duration);
            
            //drop of and increase resource
            ResourceSystem.Instance.IncreaseResource(_resourceType, 1);
        }
    }
}