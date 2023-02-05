using System;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;
using UnityEngine.EventSystems;

public class TreehouseResident : MonoBehaviour
{
    public enum QuestStage
    {
        Active, Completed, RewardObtained
    }
    
    public struct ActiveQuestEntry
    {
        public Quest Quest;
        public QuestStage Stage;
    }

    [SerializeField]
    private TreehouseRoom room;
    
    [SerializeField]
    private ResidentData data;

    public ResidentData Data => data;

    private List<ActiveQuestEntry> _activeQuests = new List<ActiveQuestEntry>();
    
    [SerializeField]
    private SpriteRenderer _renderer;

    [SerializeField]
    private GameObject _questCompleteMarker;

    public Queue<string> QueuedConversations = new Queue<string>();
    
    public TreehouseRoom Room { get; set; }

    public IReadOnlyList<ActiveQuestEntry> ActiveQuestEntries => _activeQuests;

    private void Awake()
    {
        _renderer.sprite = data.Sprite;
        foreach (var quest in data.Quests)
        {
            _activeQuests.Add(new ActiveQuestEntry(){Quest = quest});
        }
        
        // QueuedConversations.Enqueue("this is a test conversation");
        // QueuedConversations.Enqueue("I can say multiple things!");
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        ResidentInteractionSystem.Instance.BeginInteractWithResident(room);
    }

    public void CheckQuestsComplete(PlacedFurniture[] roomFurniture)
    {
        for (var i = 0; i < _activeQuests.Count; i++)
        {
            var quest = _activeQuests[i];
            if (quest.Stage == QuestStage.Active && quest.Quest.CheckComplete(roomFurniture))
            {
                quest.Stage = QuestStage.Completed;
                _activeQuests[i] = quest;
                _questCompleteMarker.SetActive(true);
                quest.Quest.QueueCompletedDialogue(this);
            }
        }
    }

    [Button]
    public void FinishAllCompletedQuests()
    {
        for (var i = 0; i < _activeQuests.Count; i++)
        {
            var quest = _activeQuests[i];
            if (quest.Stage == QuestStage.Completed)
            {
                quest.Quest.ObtainRewards();
                quest.Stage = QuestStage.RewardObtained;
                _activeQuests[i] = quest;
            }
        }
        _questCompleteMarker.SetActive(false);
    }
}
