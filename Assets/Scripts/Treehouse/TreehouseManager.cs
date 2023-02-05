using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
using UnityEngine.Events;
using DG.Tweening;

public class TreehouseManager : MonoBehaviour
{
    private const float TREE_HEIGHT_DIFF = 6.5f;

    [SerializeField]
    private int _currentHeight;

    [SerializeField]
    private float _treeGrowthSpeed = 2f;

    [SerializeField]
    private List<TreehouseRoom> _rooms;

    [SerializeField]
    private SpriteRenderer _treeTop;

    public int GetCurrentHeight => _currentHeight;

    [SerializeField]
    private Material _treeMaterial;

    private float _resourceUIAlpha = 0;

    [SerializeField]
    private CanvasGroup _resourceCanvas;
    
    public float ResourceUIAlpha
    {
        get => _resourceUIAlpha;
        set => _resourceUIAlpha = value;
    }

    public Material TreeMaterial
    {
        get => _treeMaterial;
        set => _treeMaterial = value;
    }

    [Button]
    public void IncreaseTreeHeight()
    {
        int maxHeight = _rooms.Count;

        if (_currentHeight >= maxHeight)
            return;

        _currentHeight ++;

        StartCoroutine(TweenFloor(_currentHeight - 1));

        switch (_currentHeight)
        {
            case 2:
                ProgressionManager.CompleteStep(ProgressStep.GrownTree);
                break;
            case 3:
                ProgressionManager.CompleteStep(ProgressStep.GrownTree2);
                break;
            case 4:
                ProgressionManager.CompleteStep(ProgressStep.GrownTree3);
                break;
        }
    }

    IEnumerator TweenFloor(int floorIdx)
    {
        DOTween.To(
            ()=> TreeMaterial.GetFloat("_ReplaceSlider"), 
            x=> TreeMaterial.SetFloat("_ReplaceSlider", x), 
            1, 0.5f);

        DOTween.To(
            () => _resourceUIAlpha, 
            x => _resourceUIAlpha = x, 
            0, 0.5f);

        yield return new WaitForSeconds(0.5f);

        TreehouseRoom currentRoom = _rooms[floorIdx];
        currentRoom.gameObject.SetActive(true);
        Vector3 endPos = currentRoom.transform.localPosition;

        if(floorIdx == 0)
        {
            _treeTop.transform.localScale = new Vector3(0.5f, 0, 1);
            _treeTop.transform.DOScale(new Vector3(1, 1, 1), _treeGrowthSpeed);
            _treeTop.transform.DOLocalMoveY(currentRoom.transform.localPosition.y + TREE_HEIGHT_DIFF -1.5f, _treeGrowthSpeed);

            currentRoom.transform.localPosition = new Vector3(endPos.x, endPos.y - TREE_HEIGHT_DIFF, endPos.z+2);
            currentRoom.transform.DOLocalMove(endPos, _treeGrowthSpeed);
        }
        else
        {
            TreehouseRoom previousRoom = _rooms[floorIdx - 1];
            currentRoom.transform.position = previousRoom.transform.position;
            currentRoom.transform.DOLocalMoveY(endPos.y, _treeGrowthSpeed);
            _treeTop.transform.DOLocalMoveY(endPos.y + TREE_HEIGHT_DIFF -1.5f, _treeGrowthSpeed);
        }

        yield return new WaitForSeconds(_treeGrowthSpeed + 0.2f);

        DOTween.To(
            ()=> TreeMaterial.GetFloat("_ReplaceSlider"), 
            x=> TreeMaterial.SetFloat("_ReplaceSlider", x), 
            0, 0.5f);

        DOTween.To(
            () => _resourceUIAlpha, 
            x => _resourceUIAlpha = x, 
            1, 0.5f);

    }

    // Start is called before the first frame update
    void Start()
    {
        // foreach (SpriteRenderer graphic in _floorGraphics)
        // {
        //     graphic.gameObject.SetActive(false);
        // }
        TreeMaterial.SetFloat("_ReplaceSlider", 0);
        _treeTop.transform.localScale = new Vector3(0, 0, 0);

        
        foreach (TreehouseRoom room in _rooms)
        {
            room.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _resourceCanvas.alpha = _resourceUIAlpha;
    }
}
