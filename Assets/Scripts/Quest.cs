using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Quest
{   
    [SerializeField]
    private string _clueText;

    private static List<Furniture> _reusableTempList = new List<Furniture>();

    [SerializeField]
    private string[] completionDialogue;
    
    [SerializeField]
    private List<Furniture> completionConditions;

    [SerializeField]
    private List<Furniture.FurnitureCost> rewards;

    public string ClueText => _clueText;
    
    public bool CheckComplete(PlacedFurniture[] roomFurniture)
    {
        _reusableTempList.Clear();
        _reusableTempList.AddRange(completionConditions);

        foreach (var furniture in roomFurniture)
        {
            if (furniture.IsPurchased)
            {
                if (_reusableTempList.Remove(furniture.FurnitureData) == false)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void QueueCompletedDialogue(TreehouseResident resident)
    {
        foreach (var dialogue in completionDialogue)
        {
            resident.QueuedConversations.Enqueue(dialogue);
        }
    }

    public void ObtainRewards()
    {
        foreach (var reward in rewards)
        {
            ResourceSystem.Instance.IncreaseResource(reward.Resource, reward.Amount);
        }
    }
}
