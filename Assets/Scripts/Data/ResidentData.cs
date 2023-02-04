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

    [SerializeField]
    private List<Quest> _quests;

    public string Name => _residentName;

    public Sprite Sprite => _sprite;
    
    public IReadOnlyList<Quest> Quests => _quests;
}
