using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

public class TreehouseManager : MonoBehaviour
{
    [SerializeField]
    private int _currentHeight;

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

    [Button]
    public void IncreaseTreeHeight()
    {
        int maxHeight = _rooms.Count;

        if (_currentHeight >= maxHeight)
            return;

        _currentHeight ++;

        //_floorGraphics[_currentHeight-1].gameObject.SetActive(true);
        _rooms[_currentHeight-1].gameObject.SetActive(true);
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
