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
    private Transform stickGatherPoint;
    [SerializeField]
    private Transform carrotGatherPoint;
    [SerializeField]
    private Transform stickDropOfPoint;
    [SerializeField]
    private Transform carrotDropOfPoint;
    
    [SerializeField]
    private Worker workerPrefab;

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
        worker.SetUnallocated(stickDropOfPoint.position + new Vector3(Random.Range(-2f, 2f), 0, 0));
    }

    private void AllocateWorker(Worker worker, Resource newResourceType)
    {
        switch (newResourceType)
        {
            case Resource.Carrots:
                worker.Initialize(carrotGatherPoint, carrotDropOfPoint, Resource.Carrots);
                break;
            case Resource.Sticks:
                worker.Initialize(stickGatherPoint, stickDropOfPoint, Resource.Sticks);
                break;
        }
        
        var newWorkerList = newResourceType == Resource.Carrots ? _carrotWorkers : _stickWorkers;
        newWorkerList.Add(worker);
    }

    public void IncreaseWorkerCount()
    {
        var worker = Instantiate(workerPrefab);

        worker.transform.position = stickDropOfPoint.position + new Vector3(Random.Range(-2f, 2f), 0, 0);
        
        _unallocatedWorkers.Add(worker);
        
        _workerCount++;
    }
}
