using System;
using System.Collections;
using UnityEngine;

public class Sapling : MonoBehaviour
{
    private Coroutine _waterSaplingRoutine;

    [SerializeField]
    private TreehouseManager treehouseManager;

    [SerializeField]
    private WorkerAllocationSystem workerAllocationSystem;

    [SerializeField]
    private Vector3 cameraStartPosition;

    [SerializeField]
    private float cameraStartOrthographicSize;
    
    [SerializeField]
    private Vector3 camerafirstZoomOutPosition;

    [SerializeField]
    private float cameraFirstZoomOutOrthographicSize;

    private void Start()
    {
        CameraManager.Instance.Set(cameraStartPosition, cameraStartOrthographicSize);
    }

    private void OnMouseDown()
    {
        if (_waterSaplingRoutine != null)
        {
            return;
        }
        _waterSaplingRoutine = StartCoroutine(WaterSapling());
    }

    IEnumerator WaterSapling()
    {
        yield return new WaitForSeconds(0.5f);
        // TODO animation
        
        ProgressionManager.CompleteStep(ProgressStep.WateredTree);
        treehouseManager.IncreaseTreeHeight();
        workerAllocationSystem.IncreaseWorkerCount();
        workerAllocationSystem.IncreaseWorkerCount();
        
        CameraManager.Instance.Zoom(camerafirstZoomOutPosition,  cameraFirstZoomOutOrthographicSize);
    }
}
