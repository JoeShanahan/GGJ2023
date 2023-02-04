using UnityEngine;

public class ConstructionPoint : MonoBehaviour
{
    [SerializeField]
    private TreehouseRoom room;

    [SerializeField]
    private int index;
    
    private void OnMouseDown()
    {
        FurnitureMenu.Instance.OpenMenu(this);
    }

    public void BuildFurniture(Furniture furniture)
    {
        gameObject.SetActive(false);
        Instantiate(furniture.FurnitureObject, transform.position, Quaternion.identity);
        room.BuildFurniture(furniture, index);
    }
}
