using System.Collections.Generic;
using EasyButtons;
using UnityEngine;

[DefaultExecutionOrder(1000)] // after ProgressionManager
public class GatherPoint : MonoBehaviour
{
    public ProgressStep Step;
    public Resource resourceType;
    public Transform gatherPoint => transform;
    public Transform dropOffPoint;

    private WorkerAllocateUI _allocateUI;

    [SerializeField]
    private GameObject _uiPrefab;

    void SetupUI()
    {
        _allocateUI = W2C.InstantiateAs<WorkerAllocateUI>(_uiPrefab);
        _allocateUI.Setup(resourceType);
        _allocateUI.SetPosition(gatherPoint);
    }
    
    void Start()
    {
        if (Step == ProgressStep.Unknown)
        {
            SetupUI();
        }
        else
        {
            ProgressionManager.Subscribe(OnProgressionStepComplete);
        }
    }
    
    private void OnProgressionStepComplete()
    {
        if (ProgressionManager.HasDone(Step))
        {
            SetupUI();
            ProgressionManager.Unsubscribe(OnProgressionStepComplete);
        }
    }
}