using System;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Random = UnityEngine.Random;
using System.Collections;

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

    [Header("Activity Parameters")]
    [SerializeField]
    private float _idleMaxDuration = 10;

    [SerializeField]
    private float _idleMinDuration = 1;

    [SerializeField]
    private float _walkSpeed = 2.5f;

    [SerializeField]
    private float _jumpPower = 1;

    [SerializeField]
    private float _jumpsPerSecond = 2;

    private void Awake()
    {
        _renderer.sprite = data.Sprite;
        foreach (var quest in data.Quests)
        {
            _activeQuests.Add(new ActiveQuestEntry() { Quest = quest });
        }        

        // QueuedConversations.Enqueue("this is a test conversation");
        // QueuedConversations.Enqueue("I can say multiple things!");
    }

    private void OnEnable()
    {
        StartCoroutine(ResidentRoutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    //pretty much same code aws worker routine
    public IEnumerator ResidentRoutine()
    {
        while (true)
        {
            var idleDuration = Random.Range(_idleMinDuration, _idleMaxDuration);
            yield return new WaitForSeconds(idleDuration);

            //walk to next spot
            Vector3 newRandomPos = new Vector3(Random.Range(-2.7f, 2.57f), Random.Range(1.31f, 2.67f), -0.2f);
            var distance = (transform.localPosition - newRandomPos).magnitude;
            var duration = distance / _walkSpeed;
            int numJumps = Mathf.RoundToInt(duration * _jumpsPerSecond);

            if (newRandomPos.x < transform.localPosition.x)
            {
                transform.Rotate(new Vector3(0, 0, 0));
            }
            else
            {
                transform.Rotate(new Vector3(0, 180, 0));
            }

            transform.DOLocalJump(newRandomPos + Vector3.back, _jumpPower, numJumps, duration).SetEase(Ease.Linear);
            yield return new WaitForSeconds(duration);
        }
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
