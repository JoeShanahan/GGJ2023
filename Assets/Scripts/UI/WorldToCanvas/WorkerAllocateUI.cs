using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorkerAllocateUI : W2C
{
    [SerializeField]
    private TMP_Text _numberText;

    [SerializeField]
    private RectTransform _plusButton;

    [SerializeField]
    private RectTransform _minusButton;

    [SerializeField]
    private Resource _resourceType;

    public void Setup(Resource resType)
    {
        _resourceType = resType;
    }

    public void ButtonPressPlus()
    {
        var allocator = FindObjectOfType<WorkerAllocationSystem>();
        allocator.ReallocateWorker(_resourceType);
        _numberText.text = allocator.GetWorkerCount(_resourceType).ToString();
    }

    public void ButtonPressMinus()
    {
        var allocator = FindObjectOfType<WorkerAllocationSystem>();
        allocator.RemoveWorker(_resourceType);
        _numberText.text = allocator.GetWorkerCount(_resourceType).ToString();
    }
}
