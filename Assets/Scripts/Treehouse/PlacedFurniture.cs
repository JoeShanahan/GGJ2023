using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedFurniture : MonoBehaviour
{
    [SerializeField]
    private Furniture _furnitureData;

    public Furniture FurnitureData => _furnitureData;

    public bool IsPurchased => gameObject.activeSelf;
}
