using UnityEngine;

public class CollectResourceTriggerProgression : MonoBehaviour
{
    [SerializeField]
    private Resource resource;
    [SerializeField]
    private int amount;
    [SerializeField]
    private ProgressStep step;

    public void Update()
    {
        if (ResourceSystem.Instance.GetResourceCount(resource) >= amount)
        {
            ProgressionManager.CompleteStep(step);
        }
        Destroy(this);
    }
}
