using System.Collections.Generic;
using EasyButtons;
using UnityEngine;

public class WorkerAllocationSystem : MonoBehaviour
{
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
        var oldWorkerList = newResourceType == Resource.Carrots ? _stickWorkers : _carrotWorkers;
        if (oldWorkerList.Count == 0)
        {
            return;
        }

        var worker = oldWorkerList[^1];
        oldWorkerList.RemoveAt(oldWorkerList.Count - 1);
        
        AllocateWorker(worker, newResourceType);
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

    private void IncreaseWorkerCount()
    {
        var worker = Instantiate(workerPrefab);

        if (_workerCount % 2 == 0)
        {
            worker.transform.position = carrotDropOfPoint.position;
            AllocateWorker(worker, Resource.Carrots);
        }
        else
        {
            worker.transform.position = stickDropOfPoint.position;
            AllocateWorker(worker, Resource.Sticks);
        }
        
        _workerCount++;
    }
}
