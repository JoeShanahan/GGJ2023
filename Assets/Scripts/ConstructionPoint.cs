using UnityEngine;
using UnityEngine.EventSystems;

public class ConstructionPoint : MonoBehaviour
{
    [SerializeField]
    private TreehouseRoom room;

    [SerializeField]
    private int index;
    
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        FurnitureMenu.Instance.OpenMenu(this);
    }

    public void BuildFurniture(Furniture furniture)
    {
        gameObject.SetActive(false);
        Instantiate(furniture.FurnitureObject, transform.position, Quaternion.identity);
        room.BuildFurniture(furniture, index);
    }
}
