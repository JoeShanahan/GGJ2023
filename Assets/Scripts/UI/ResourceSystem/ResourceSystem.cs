using System;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public enum Resource
{
    Carrots = 0,
    Sticks = 1,
    Hearts = 2,
    Stone = 3,
    ResourceCount = 4,
}

public class ResourceSystem : MonoBehaviour
{
    public static ResourceSystem Instance;
    
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
    
    public bool TryDecreaseResources(List<Furniture.FurnitureCost> costs)
    {
        foreach (var cost in costs)
        {
            if (_resources[(int)cost.Resource] < cost.Amount)
            {
                return false;
            }
        }

        foreach (var cost in costs)
        {
            _resources[(int)cost.Resource] -= cost.Amount;
            resourceCounters[(int)cost.Resource].OnChange(-cost.Amount);
        }
        
        return true;
    }

    private void Awake()
    {
        Assert.IsNull(Instance);
        Instance = this;

        for (int i = 0; i < resourceCounters.Length; i++)
        {
            resourceCounters[i].Initialize((Resource)i);
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}