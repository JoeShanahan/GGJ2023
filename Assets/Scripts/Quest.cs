using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Quest
{
    private static List<Furniture> _reusableTempList = new List<Furniture>();

    [SerializeField]
    private List<Furniture> completionConditions;

    [SerializeField]
    private List<Furniture.FurnitureCost> rewards;

    // roomFurniture is an array that can contain null entries for the construction points where nothing has been built.
    // completionConditions can contain duplicates if the goal is to need 2 of x to complete the quest. That's why the temp list is needed for checking.
    public bool CheckComplete(Furniture[] roomFurniture)
    {
        _reusableTempList.Clear();
        _reusableTempList.AddRange(completionConditions);

        foreach (var furniture in roomFurniture)
        {
            if (furniture != null)
            {
                if (_reusableTempList.Remove(furniture) == false)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void ObtainRewards()
    {
        foreach (var reward in rewards)
        {
            ResourceSystem.Instance.IncreaseResource(reward.Resource, reward.Amount);
        }
    }
}