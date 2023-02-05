using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ResidentInteractionSystem : MonoBehaviour
{
    public static ResidentInteractionSystem Instance;
    
    private const float k_charactersPerSecond = 12f;

    private Coroutine _interactionCoroutine;

    private TreehouseRoom _activeInteractionRoom;

    [SerializeField]
    private ResidentQuestPanel residentQuestPanel;

    public bool IsInActiveInteraction => _activeInteractionRoom != null;
    
    private void Awake()
    {
        Assert.IsNull(Instance);
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void BeginInteractWithResident(TreehouseRoom room)
    {
        if (_activeInteractionRoom != null)
        {
            return;
        }

        _activeInteractionRoom = room;
        
        CameraManager.Instance.ZoomIn(room);
        if (_interactionCoroutine != null)
        {
            StopCoroutine(_interactionCoroutine);
        }
        _interactionCoroutine = StartCoroutine(InteractionRoutine(room.Resident));
    }

    public void EndInteractWithResident()
    {
        Assert.IsNotNull(_activeInteractionRoom);
        _activeInteractionRoom = null;
        residentQuestPanel.MakeVisible(false);
        CameraManager.Instance.ZoomOut();
    }

    IEnumerator InteractionRoutine(TreehouseResident resident)
    {
        while(resident.QueuedConversations.TryDequeue(out string conversation))
        {
            var duration = conversation.Length / k_charactersPerSecond;
            SpeechBubble.Create(resident.transform, new Vector3(0, resident.transform.localPosition.y + 0.5f, 0), conversation, duration);
            yield return new WaitForSeconds(duration + 0.5f);
        }
        _interactionCoroutine = null;
        resident.FinishAllCompletedQuests();
        residentQuestPanel.SetResident(resident);
        residentQuestPanel.MakeVisible(true);
    }
}
