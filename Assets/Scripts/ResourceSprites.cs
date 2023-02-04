using System;
using UnityEngine;

[CreateAssetMenu]
public class ResourceSprites : ScriptableObject
{
    [SerializeField]
    private Sprite carrotSprite;
    [SerializeField]
    private Sprite stickSprite;
    [SerializeField]
    private Sprite heartSprite;
    [SerializeField]
    private Sprite stoneSprite;

    public Sprite GetResourceSprite(Resource resource)
    {
        switch (resource)
        {
            case Resource.Carrots:
                return carrotSprite;
            case Resource.Sticks:
                return stickSprite;
            case Resource.Hearts:
                return heartSprite;
            case Resource.Stone:
                return stoneSprite;
            default:
                throw new NotImplementedException();
        }
    }
}
