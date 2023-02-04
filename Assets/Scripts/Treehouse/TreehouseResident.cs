using System.Collections.Generic;
using EasyButtons;
using UnityEngine;

public class TreehouseResident : MonoBehaviour
{
    private struct ActiveQuestEntry
    {
        public Quest Quest;
        public bool Completed;
    }
    
    [SerializeField]
    private ResidentData data;

    private List<ActiveQuestEntry> _activeQuests = new List<ActiveQuestEntry>();
    
    [SerializeField]
    private SpriteRenderer _renderer;

    [SerializeField]
    private GameObject _questCompleteMarker;

    private void Awake()
    {
        _renderer.sprite = data.Sprite;
        foreach (var quest in data.Quests)
        {
            _activeQuests.Add(new ActiveQuestEntry(){Quest = quest});
        }
    }

    public void CheckQuestsComplete(Furniture[] roomFurniture)
    {
        for (var i = 0; i < _activeQuests.Count; i++)
        {
            var quest = _activeQuests[i];
            if (quest.Quest.CheckComplete(roomFurniture))
            {
                quest.Completed = true;
                _activeQuests[i] = quest;
                _questCompleteMarker.SetActive(true);
            }
        }
    }

    [Button]
    public void DEBUG_FinishAllCompletedQuests()
    {
        for (var i = _activeQuests.Count - 1; i >= 0; i--)
        {
            var quest = _activeQuests[i];
            if (quest.Completed)
            {
                quest.Quest.ObtainRewards();
                _activeQuests.RemoveAt(i);
            }
        }
        _questCompleteMarker.SetActive(false);
    }
}