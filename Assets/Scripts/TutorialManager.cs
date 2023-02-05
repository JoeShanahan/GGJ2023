using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using EasyButtons;
using WorldToCanvas;

public class TutorialManager : MonoBehaviour
{
    public enum TutorialID { Unknown, WaterTheTree, WaterMeter, ClickResourceToCollect, ClickResident, DoQuests, SpreadRoots };

    [SerializeField]
    private float _zoomTime;

    [SerializeField]
    private Ease _easeIn;

    [SerializeField]
    private Ease _easeOut;

    [SerializeField]
    private SkinnedMeshRenderer _peepholeMesh;

    private TutorialText _tutorialText;

    private List<TutorialID> _shownTutorials = new List<TutorialID>();

    [System.Serializable]
    public class TutorialEntry
    {
        public TutorialID tutorialID;
        public Transform focusTransform;
        public float peepholePercent;
        public Vector2 textOffset;

        [TextArea(minLines:2, maxLines:5)]
        public string text;
    }

    [SerializeField]
    private List<TutorialEntry> entries;


    [Button]
    public void EnableTutorial(TutorialID tutID)
    {
        foreach (TutorialEntry entry in entries)
        {
            if (entry.tutorialID != tutID)
                continue;

            if (entry.focusTransform == null || entry.peepholePercent == 0)
                return;

                _tutorialText?.Dismiss();
                _tutorialText = null;     

            Vector3 focusPos = entry.focusTransform.position;
            focusPos = new Vector3(focusPos.x, focusPos.y, _peepholeMesh.transform.position.z);

            // _peepholeMesh.transform.DOMove(focusPos, _zoomTime).SetEase(_easeIn);
            _peepholeMesh.transform.position = focusPos;
            DOTween.To(()=> _peepholeMesh.GetBlendShapeWeight(0), x=> _peepholeMesh.SetBlendShapeWeight(0, x), entry.peepholePercent, _zoomTime).SetEase(_easeIn);

            _tutorialText = W2CManager.TutorialText(entry.focusTransform.position, entry.text, entry.textOffset);
            break;
        }

    }

    [Button]
    public void DismissTutorial()
    {
        DOTween.To(()=> _peepholeMesh.GetBlendShapeWeight(0), x=> _peepholeMesh.SetBlendShapeWeight(0, x), 0, _zoomTime).SetEase(_easeOut);

        _tutorialText?.Dismiss();
        _tutorialText = null;        

    }

    // Start is called before the first frame update
    void Start()
    {
        ProgressionManager.Subscribe(OnProgress);
    }

    void OnProgress()
    {
        if (ProgressionManager.HasDone(ProgressStep.WateredTree) && _shownTutorials.Contains(TutorialID.ClickResourceToCollect) == false)
        {
            _shownTutorials.Add(TutorialID.ClickResourceToCollect);
            StartCoroutine(ShowResourceTut());
        }
    }

    IEnumerator ShowResourceTut()
    {
        yield return new WaitForSeconds(6);
        EnableTutorial(TutorialID.ClickResourceToCollect);
        yield return new WaitForSeconds(5);
        DismissTutorial();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

