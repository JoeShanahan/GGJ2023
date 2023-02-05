using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FurnitureMenu : MonoBehaviour
{
    public static FurnitureMenu Instance;
    
    private OpenFurnitureMenuContext _context;

    private List<GameObject> _menuEntries = new List<GameObject>();

    [SerializeField]
    private GameObject _visuals;
    
    [SerializeField]
    private FurnitureMenuEntry entryPrefab;

    [SerializeField]
    private Transform content;

    public void OpenMenu(TreehouseRoom room)
    {
        foreach (var entry in _menuEntries)
        {
            Destroy(entry);
        }
        _menuEntries.Clear();
        
        for (var i = 0; i < room.AllFurniture.Count; i++)
        {
            var furniture = room.AllFurniture[i];
            if (furniture.IsPurchased)
            {
                continue;
            }
            var item = Instantiate(entryPrefab, content);
            item.Initialize(this, furniture.FurnitureData, i);
            _menuEntries.Add(item.gameObject);
        }

        // if (ResidentInteractionSystem.Instance.IsInActiveInteraction)
        // {
        //     return;
        // }
        Assert.AreEqual(_context, default);
        _context = new OpenFurnitureMenuContext()
        {
            Room = room,
        };
        _visuals.SetActive(true);
    }

    public void OnFurnitureSelected(Furniture furniture, int index)
    {
        if (ResourceSystem.Instance.TryDecreaseResources(furniture.Cost))
        {
            _visuals.SetActive(false);
            _context.Room.BuildFurniture(index);
            _context = default;
        }
    }

    public void OnCloseButtonPressed()
    {
        _visuals.SetActive(false);
        _context = default;
    }
    
    private void OnDestroy()
    {
        Instance = null;
    }
    
    private void Awake()
    {
        Assert.IsNull(Instance);
        Instance = this;
    }
}

public struct OpenFurnitureMenuContext
{
    public TreehouseRoom Room;
}