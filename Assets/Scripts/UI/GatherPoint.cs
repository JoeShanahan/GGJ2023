using System.Collections.Generic;
using EasyButtons;
using UnityEngine;


public class GatherPoint : MonoBehaviour
{
    public Resource resourceType;
    public Transform gatherPoint => transform;
    public Transform dropOffPoint;

    private WorkerAllocateUI _allocateUI;

    [SerializeField]
    private GameObject _uiPrefab;

    void Start()
    {
        _allocateUI = W2C.InstantiateAs<WorkerAllocateUI>(_uiPrefab);
        _allocateUI.Setup(resourceType);
        _allocateUI.SetPosition(gatherPoint);
    }
}