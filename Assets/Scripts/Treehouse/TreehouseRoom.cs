using UnityEngine;

public class TreehouseRoom : MonoBehaviour
{
    private Furniture[] _furnitures = new Furniture[4];
    
    [SerializeField]
    private TreehouseResident _resident;

    public TreehouseResident Resident => _resident;

    [SerializeField]
    private PlacedFurniture[] _allFurniture;

    void Start()
    {
        foreach (PlacedFurniture furn in _allFurniture)
        {
            furn.gameObject.SetActive(false);
        }   
    }

    public void BuildFurniture(Furniture furniture, int index)
    {
        _furnitures[index] = furniture;
        _resident.CheckQuestsComplete(_furnitures);
    }
}
