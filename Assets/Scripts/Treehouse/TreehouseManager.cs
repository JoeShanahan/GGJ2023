using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
using UnityEngine.Events;
using DG.Tweening;

public class TreehouseManager : MonoBehaviour
{
    private const float TREE_HEIGHT_DIFF = 5F;

    [SerializeField]
    private int _currentHeight;

    [SerializeField]
    private float _treeGrowthSpeed = 2f;

    [SerializeField]
    private List<TreehouseRoom> _rooms;

    [Header("Background Graphics")]
    [SerializeField]
    private SpriteRenderer _stumpGraphic;

    [SerializeField]
    private SpriteRenderer[] _floorGraphics;

    [SerializeField]
    private SpriteRenderer[] _rootsGraphics;

    [SerializeField]
    private SpriteRenderer[] _branchesGraphics;

    public int GetCurrentHeight => _currentHeight;

    [Button]
    public void IncreaseTreeHeight()
    {
        int maxHeight = _rooms.Count;

        if (_currentHeight >= maxHeight)
            return;

        _currentHeight ++;

        TweenFloor(_rooms[_currentHeight - 1].gameObject);

        if (_currentHeight == 2)
        {
            ProgressionManager.CompleteStep(ProgressStep.GrownTree);
        }

    }

    void TweenFloor(GameObject floorGO)
    {
        floorGO.SetActive(true);
        Vector3 to = floorGO.transform.localPosition;

        floorGO.transform.localPosition = new Vector3(to.x, to.y - TREE_HEIGHT_DIFF, to.z);

        floorGO.transform.DOLocalMoveY(to.y, _treeGrowthSpeed);
    }

    // Start is called before the first frame update
    void Start()
    {
        // foreach (SpriteRenderer graphic in _floorGraphics)
        // {
        //     graphic.gameObject.SetActive(false);
        // }

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
