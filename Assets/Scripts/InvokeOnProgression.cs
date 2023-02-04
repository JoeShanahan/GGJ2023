using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(1000)] // after ProgressionManager
public class InvokeOnProgression : MonoBehaviour
{
    [SerializeField]
    private ProgressStep step;
    [SerializeField]
    private UnityEvent unityEvent;

    private void Start()
    {
        if (step == ProgressStep.Unknown)
        {
            unityEvent.Invoke();
        }
        else
        {
            ProgressionManager.Subscribe(OnProgressionStepComplete);
        }

    }

    private void OnProgressionStepComplete()
    {
        if (ProgressionManager.HasDone(step))
        {
            unityEvent.Invoke();
        }
    }
}
