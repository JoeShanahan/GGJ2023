using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

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

    private bool _initialized;

    private void Start()
    {
        ProgressionManager.Subscribe(ProgressDone);
    }

    private void ProgressDone()
    {
        if (_initialized)
            return;
        
        if (ProgressionManager.HasDone(ProgressStep.DismissedTitle))
        {
            CameraManager.Instance.Zoom(cameraStartPosition, cameraStartOrthographicSize, 3);
            _initialized = true;
            StartCoroutine(IntroRoutine());
        }
    }

    private IEnumerator IntroRoutine()
    {
        yield return new WaitForSeconds(5);
        FindObjectOfType<TutorialManager>().EnableTutorial(TutorialManager.TutorialID.WaterTheTree);
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (_waterSaplingRoutine != null)
        {
            return;
        }
        _waterSaplingRoutine = StartCoroutine(WaterSapling());
    }

    IEnumerator WaterSapling()
    {
        FindObjectOfType<TutorialManager>().DismissTutorial();

        yield return new WaitForSeconds(0.5f);
        // TODO animation
        
        ProgressionManager.CompleteStep(ProgressStep.WateredTree);
        treehouseManager.IncreaseTreeHeight();
        workerAllocationSystem.IncreaseWorkerCount();
        workerAllocationSystem.IncreaseWorkerCount();
        workerAllocationSystem.IncreaseWorkerCount();
        
        CameraManager.Instance.Zoom(camerafirstZoomOutPosition,  cameraFirstZoomOutOrthographicSize);
    }
}
