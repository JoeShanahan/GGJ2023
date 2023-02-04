using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ResidentData : ScriptableObject
{
    [SerializeField]
    private string _residentName;

    [SerializeField]
    private Sprite _sprite;

    public string Name => _residentName;

    public Sprite Sprite => _sprite;
}
