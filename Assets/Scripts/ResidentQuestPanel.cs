using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using EasyButtons;
using TMPro;
using UnityEngine.UI;

public class ResidentQuestPanel : MonoBehaviour
{
    private TreehouseResident _resident;
    
    [SerializeField]
    private float _activeX;

    [SerializeField]
    private float _inactiveX;

    [SerializeField]
    private float _moveTime = 0.5f;

    [SerializeField]
    private Ease _moveEase = Ease.OutExpo;

    [SerializeField]
    private TMP_Text _nameText;

    [SerializeField]
    private TMP_Text[] _questTexts;

    [SerializeField]
    private Image[] _starBackgrounds;

    [SerializeField]
    private RectTransform[] _stars;

    [SerializeField]
    private Color _nonCompleteCircleColour;

    [SerializeField]
    private Color _completeCircleColour;


    [Button]
    public void MakeVisible(bool yesNo)
    {
        float newX = yesNo ? _activeX : _inactiveX;
        (transform as RectTransform).DOAnchorPosX(newX, _moveTime).SetEase(Ease.OutExpo);
    }

    void Start()
    {
        var rt = (transform as RectTransform);
        rt.anchoredPosition = new Vector2(_inactiveX, rt.anchoredPosition.y);
    }
    
    
    public void OpenBuildMenu()
    {
        FurnitureMenu.Instance.OpenMenu(_resident.Room);
    }
    
    
    [Button]
    public void SetResident(TreehouseResident resident)
    {
        _resident = resident;
        var data = resident.Data;
        _nameText.text = data.Name;
        bool hasRevealedOne = false;

        for (int i=0; i<3; i++)
        {
            var questEntry = resident.ActiveQuestEntries[i];
            var quest = questEntry.Quest;
            
            bool isComplete = questEntry.Stage != TreehouseResident.QuestStage.Active;
            _starBackgrounds[i].color = isComplete ? _completeCircleColour : _nonCompleteCircleColour;
            _stars[i].gameObject.SetActive(isComplete);

            if (isComplete == false)
            {
                if (hasRevealedOne == false)
                {
                    hasRevealedOne = true;
                    _questTexts[i].text = quest.ClueText;
                }
                else
                {
                    // yes this is horribly inefficient no i don't care
                    string newString = "";

                    foreach (char c in quest.ClueText)
                    {
                        newString += c == ' ' ? ' ' : "?";
                    }

                    _questTexts[i].text = newString;
                }
            }
            else
            {
                _questTexts[i].text = quest.ClueText;
            }
        }
    }
}
