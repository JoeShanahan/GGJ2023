using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Furniture : ScriptableObject
{
    [Serializable]
    public struct FurnitureCost
    {
        public Resource Resource;
        public int Amount;
    }
    
    public Sprite IconSprite;
    public List<FurnitureCost> Cost;
}
