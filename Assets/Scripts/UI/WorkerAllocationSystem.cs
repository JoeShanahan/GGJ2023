using System.Collections.Generic;
using DG.Tweening;
using EasyButtons;
using TMPro;
using UnityEngine;

public class WorkerAllocationSystem : MonoBehaviour
{
    private List<Worker> _unallocatedWorkers = new List<Worker>();
    
    private List<Worker> _stickWorkers = new List<Worker>();

    private List<Worker> _carrotWorkers = new List<Worker>();
    
    private List<Worker> _stoneWorkers = new List<Worker>();

    private int _workerCount;

    [SerializeField]
    private int[] workerIncreaseCosts;

    [SerializeField]
    private int startWorkerCount;

    [SerializeField]
    private GatherPoint _carrotPoint;
    
    [SerializeField]
    private GatherPoint _stickPoint;
    
    [SerializeField]
    private GatherPoint _stonePoint;
    
    [SerializeField]
    private Worker workerPrefab;
    
    [SerializeField]
    private GameObject _increaseWorkersButton;

    [SerializeField]
    private TMP_Text _increaseWorkerCostText;

    [SerializeField]
    private float workerSpawnJumpPower;

    public int GetWorkerCount(Resource resType)
    {
        if (resType == Resource.Carrots)
            return _carrotWorkers.Count;
        
        if (resType == Resource.Sticks)
            return _stickWorkers.Count;
        
        if (resType == Resource.Stone)
            return _stoneWorkers.Count;
        
        return 0;
    }

    public int GetWorkerIncreaseCost()
    {
        if (_workerCount >= workerIncreaseCosts.Length)
        {
            return 0;
        }
        return workerIncreaseCosts[_workerCount];
    }

    public void TryBuyWorker()
    {
        if (ResourceSystem.Instance.TryDecreaseResource(Resource.Hearts, GetWorkerIncreaseCost()))
        {
            IncreaseWorkerCount();
        }
    }

    public void Start()
    {
        for (int i = 0; i < startWorkerCount; i++)
        {
            IncreaseWorkerCount();
        }

        _increaseWorkerCostText.text = GetWorkerIncreaseCost().ToString() + "x";
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
        var oldWorkerList = resourceType switch
        {
            Resource.Carrots => _carrotWorkers,
            Resource.Sticks => _stickWorkers,
            _ => _stoneWorkers
        };
        if (oldWorkerList.Count == 0)
        {
            return;
        }
        
        var worker = oldWorkerList[^1];
        oldWorkerList.RemoveAt(oldWorkerList.Count - 1);
        
        _unallocatedWorkers.Add(worker);
        worker.SetUnallocated(_stickPoint.dropOffPoint.position + new Vector3(Random.Range(-2f, 2f), 0, 0));
    }

    private void AllocateWorker(Worker worker, Resource resourceType)
    {
        switch (resourceType)
        {
            case Resource.Carrots:
                worker.Initialize(_carrotPoint.gatherPoint, _carrotPoint.dropOffPoint, _carrotPoint.resourceType);
                break;
            case Resource.Sticks:
                worker.Initialize(_stickPoint.gatherPoint, _stickPoint.dropOffPoint, _stickPoint.resourceType);
                break;
            case Resource.Stone:
                worker.Initialize(_stonePoint.gatherPoint, _stonePoint.dropOffPoint, _stonePoint.resourceType);
                break;
        }
        
        var newWorkerList = resourceType switch
        {
            Resource.Carrots => _carrotWorkers,
            Resource.Sticks => _stickWorkers,
            _ => _stoneWorkers
        };
        newWorkerList.Add(worker);
    }

    public void IncreaseWorkerCount()
    {
        var worker = Instantiate(workerPrefab);
        worker.transform.position = _stickPoint.dropOffPoint.position + new Vector3(Random.Range(-2f, 2f), 0, 0);
        worker.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        worker.transform.DOJump(worker.transform.position, workerSpawnJumpPower, 1, 1f);
        worker.transform.DOScale(Vector3.one, 1f);
        
        _unallocatedWorkers.Add(worker);
        worker.SetUnallocated(worker.transform.position);
        
        _workerCount++;

        if (_workerCount >= workerIncreaseCosts.Length)
        {
            _increaseWorkersButton.SetActive(false);
        }
        else
        {
            _increaseWorkerCostText.text = GetWorkerIncreaseCost().ToString() + "x";
        }
    }
}
