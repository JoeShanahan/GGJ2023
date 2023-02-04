using UnityEngine;
using TMPro;

/// <summary>
/// Code-monkey inspired chat bubble system
/// </summary>
public class SpeechBubble : MonoBehaviour
{
    /// <summary>
    /// Creates a chat bubble for an animal
    /// </summary>
    /// <param name="parent">The animal to speak</param>
    /// <param name="localPosition">The position of the chat bubble</param>
    /// <param name="text">The text inside the speech bubble</param>
    /// <param name="lifetime">The lifetime of the speech bubble</param>
    public static void Create(Transform parent, Vector3 localPosition, string text, float lifetime)
    {
        if (parent.GetComponentInChildren<SpeechBubble>())
        {
            Destroy(parent.GetComponentInChildren<GameObject>());
        }

        Transform speechBubbleTransform = Instantiate(GameAssets.i._pfSpeechBubble, parent);
        speechBubbleTransform.localPosition = localPosition;

        speechBubbleTransform.GetComponent<SpeechBubble>().SetupSpeechBubble(text);

        Destroy(speechBubbleTransform.gameObject, lifetime);
    }

    [SerializeField]
    private SpriteRenderer _bgSpriteRenderer;

    [SerializeField]
    private TextMeshPro _textMeshPro;

    [Header("Speech Bubble Values")]
    [SerializeField]
    private Vector2 _speechBubblePadding;

    private void SetupSpeechBubble(string text)
    {
        _textMeshPro.SetText(text);
        _textMeshPro.ForceMeshUpdate();

        Vector2 textSize = _textMeshPro.GetRenderedValues(false);

        _bgSpriteRenderer.size = textSize + _speechBubblePadding;

        _bgSpriteRenderer.transform.localPosition =
            new Vector3(_bgSpriteRenderer.size.x / 2f, 0f);
    }
}
