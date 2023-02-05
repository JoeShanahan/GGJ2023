using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FurnitureMenuEntry : MonoBehaviour
{
    private int _index;
    
    [Serializable]
    private struct CostEntry
    {
        public GameObject Object;
        public Image Image;
        public TMP_Text CostText;
    }
    
    private Furniture _furniture;

    private FurnitureMenu _furnitureMenu;
    
    [SerializeField]
    private Image furnitureIcon;
    
    [SerializeField]
    private List<CostEntry> costEntries;

    [SerializeField]
    private ResourceSprites resourceSprites;
    
    public void OnFurnitureSelected()
    {
        _furnitureMenu.OnFurnitureSelected(_furniture, _index);
    }

    public void Initialize(FurnitureMenu furnitureMenu, Furniture furniture, int index)
    {
        _index = index;
        _furnitureMenu = furnitureMenu;
        _furniture = furniture;
        furnitureIcon.sprite = furniture.IconSprite;

        for (var i = 0; i < costEntries.Count; i++)
        {
            var entry = costEntries[i];
            if (furniture.Cost.Count - 1 < i)
            {
                entry.Object.SetActive(false);
            }
            else
            {
                entry.Object.SetActive(true);
                var cost = furniture.Cost[i];
                entry.Image.sprite = resourceSprites.GetResourceSprite(cost.Resource);
                entry.CostText.text = furniture.Cost[i].Amount.ToString();
            }
        }
    }
}
