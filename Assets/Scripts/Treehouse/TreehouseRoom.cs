using System.Collections.Generic;
using UnityEngine;

public class TreehouseRoom : MonoBehaviour
{
   // private Furniture[] _furnitures = new Furniture[4];
    
    [SerializeField]
    private TreehouseResident _resident;

    public TreehouseResident Resident => _resident;

    [SerializeField]
    private PlacedFurniture[] _allFurniture;

    public IReadOnlyList<PlacedFurniture> AllFurniture => _allFurniture;

    void Start()
    {
        foreach (PlacedFurniture furn in _allFurniture)
        {
            furn.gameObject.SetActive(false);
        }

        _resident.Room = this;
    }

    public void BuildFurniture(int index)
    {
        _allFurniture[index].gameObject.SetActive(true);
        _resident.CheckQuestsComplete(_allFurniture);
    }
}
