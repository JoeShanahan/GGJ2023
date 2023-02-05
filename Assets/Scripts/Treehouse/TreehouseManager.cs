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

    [Button]
    public void IncreaseTreeHeight()
    {
        int maxHeight = _rooms.Count;

        if (_currentHeight >= maxHeight)
            return;

        _currentHeight ++;

        StartCoroutine(TweenFloor(_currentHeight - 1));

        if (_currentHeight == 2)
        {
            ProgressionManager.CompleteStep(ProgressStep.GrownTree);
        }
    }

    IEnumerator TweenFloor(int floorIdx)
    {
        DOTween.To(
            ()=> _treeMaterial.GetFloat("_ReplaceSlider"), 
            x=> _treeMaterial.SetFloat("_ReplaceSlider", x), 
            1, 0.5f);

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
            ()=> _treeMaterial.GetFloat("_ReplaceSlider"), 
            x=> _treeMaterial.SetFloat("_ReplaceSlider", x), 
            0, 0.5f);

    }

    // Start is called before the first frame update
    void Start()
    {
        // foreach (SpriteRenderer graphic in _floorGraphics)
        // {
        //     graphic.gameObject.SetActive(false);
        // }
        _treeMaterial.SetFloat("_ReplaceSlider", 0);
        _treeTop.transform.localScale = new Vector3(0, 0, 0);

        
        foreach (TreehouseRoom room in _rooms)
        {
            room.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
