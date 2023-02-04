using UnityEngine;

/// <summary>
/// Code-monkey inspired game asset system
/// </summary>
public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if (_i == null)
            {
                _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();                
            }
            return _i;
        }
    }

    public Transform _pfSpeechBubble;    
}
