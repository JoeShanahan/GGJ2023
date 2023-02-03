using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreehouseResident : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _renderer;

    private ResidentData _data;

    public void SetResident(ResidentData resident)
    {
        _data = resident;
        _renderer.sprite = resident.Sprite;
    }
}
