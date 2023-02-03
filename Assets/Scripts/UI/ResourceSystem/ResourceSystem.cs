using EasyButtons;
using UnityEngine;
using UnityEngine.Serialization;

public enum Resource
{
    Carrots = 0,
    Sticks = 1,
    Hearts = 2,
    ResourceCount = 3,
}

public class ResourceSystem : MonoBehaviour
{
    private int[] _resources = new int[(int)Resource.ResourceCount];
    
    [SerializeField]
    private ResourceCounterUI[] resourceCounters;
    
    [Button]
    public void IncreaseResource(Resource resource, int value)
    {
        _resources[(int)resource] += value;
        resourceCounters[(int)resource].OnChange(value);
    }

    [Button]
    public bool TryDecreaseResource(Resource resource, int value)
    {
        if (_resources[(int)resource] < value)
        {
            return false;
        }

        _resources[(int)resource] -= value;
        resourceCounters[(int)resource].OnChange(-value);
        return true;
    }
}
