using UnityEngine;
using DG.Tweening;
using EasyButtons;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    [SerializeField]
    private float _activeX;

    [SerializeField]
    private float _inactiveX;

    [SerializeField]
    private float _moveTime = 0.5f;

    [SerializeField]
    private Ease _moveEase = Ease.OutExpo;

    [Button]
    public void MakeVisible(bool yesNo)
    {
        float newX = yesNo ? _activeX : _inactiveX;
        (transform as RectTransform).DOAnchorPosX(newX, _moveTime).SetEase(Ease.OutExpo);
    }

    void Start()
    {
        var rt = (transform as RectTransform);
        rt.anchoredPosition = new Vector2(_inactiveX, rt.anchoredPosition.y);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MakeVisible(true);
        }
    }
}
