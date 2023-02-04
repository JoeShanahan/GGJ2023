using System.Collections.Generic;
using EasyButtons;
using UnityEngine;

public class WorkerAllocationSystem : MonoBehaviour
{
    private List<Worker> _unallocatedWorkers = new List<Worker>();
    
    private List<Worker> _stickWorkers = new List<Worker>();

    private List<Worker> _carrotWorkers = new List<Worker>();

    private int _workerCount;

    [SerializeField]
    private int startWorkerCount;

    [SerializeField]
    private GatherPoint _carrotPoint;
    
    [SerializeField]
    private GatherPoint _stickPoint;
    
    [SerializeField]
    private Worker workerPrefab;

    public int GetWorkerCount(Resource resType)
    {
        if (resType == Resource.Carrots)
            return _carrotWorkers.Count;
        
        if (resType == Resource.Sticks)
            return _stickWorkers.Count;
        
        return 0;
    }

    public void Start()
    {
        for (int i = 0; i < startWorkerCount; i++)
        {
            IncreaseWorkerCount();
        }
    }

    [Button]
    public void ReallocateWorker(Resource newResourceType)
    {
        var oldWorkerList = _unallocatedWorkers; //newResourceType == Resource.Carrots ? _stickWorkers : _carrotWorkers;
        if (oldWorkerList.Count == 0)
        {
            return;
        }

        var worker = oldWorkerList[^1];
        oldWorkerList.RemoveAt(oldWorkerList.Count - 1);
        
        AllocateWorker(worker, newResourceType);
    }

    [Button]
    public void RemoveWorker(Resource resourceType)
    {
        var oldWorkerList = resourceType == Resource.Carrots ? _carrotWorkers : _stickWorkers;
        if (oldWorkerList.Count == 0)
        {
            return;
        }
        
        var worker = oldWorkerList[^1];
        oldWorkerList.RemoveAt(oldWorkerList.Count - 1);
        
        _unallocatedWorkers.Add(worker);
        worker.SetUnallocated(_stickPoint.dropOffPoint.position + new Vector3(Random.Range(-2f, 2f), 0, 0));
    }

    private void AllocateWorker(Worker worker, Resource newResourceType)
    {
        switch (newResourceType)
        {
            case Resource.Carrots:
                worker.Initialize(_carrotPoint.gatherPoint, _carrotPoint.dropOffPoint, _carrotPoint.resourceType);
                break;
            case Resource.Sticks:
                worker.Initialize(_stickPoint.gatherPoint, _stickPoint.dropOffPoint, _stickPoint.resourceType);
                break;
        }
        
        var newWorkerList = newResourceType == Resource.Carrots ? _carrotWorkers : _stickWorkers;
        newWorkerList.Add(worker);
    }

    public void IncreaseWorkerCount()
    {
        var worker = Instantiate(workerPrefab);

        worker.transform.position = _stickPoint.dropOffPoint.position + new Vector3(Random.Range(-2f, 2f), 0, 0);
        
        _unallocatedWorkers.Add(worker);
        
        _workerCount++;
    }
}
