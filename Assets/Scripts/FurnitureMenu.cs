using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FurnitureMenu : MonoBehaviour
{
    public static FurnitureMenu Instance;
    
    private OpenFurnitureMenuContext _context;
    
    [SerializeField]
    private GameObject _visuals;
    
    [SerializeField]
    private List<Furniture> furnitures;

    [SerializeField]
    private FurnitureMenuEntry entryPrefab;

    [SerializeField]
    private Transform content;

    public void OpenMenu(ConstructionPoint point)
    {
        Assert.AreEqual(_context, default);
        _context = new OpenFurnitureMenuContext()
        {
            ConstructionPoint = point,
        };
        _visuals.SetActive(true);
    }

    public void OnFurnitureSelected(Furniture furniture)
    {
        if (ResourceSystem.Instance.TryDecreaseResources(furniture.Cost))
        {
            _visuals.SetActive(false);
            _context.ConstructionPoint.BuildFurniture(furniture);
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
        
        foreach (var furniture in furnitures)
        {
            var item = Instantiate(entryPrefab, content);
            item.Initialize(this, furniture);
        }
    }
}

public struct OpenFurnitureMenuContext
{
    public ConstructionPoint ConstructionPoint;
}