using System;
using TMPro;
using UnityEngine;
using DG.Tweening;
using EasyButtons;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ResourceCounterUI : MonoBehaviour
{
    private int _currentDisplayed;

    private int _count;

    [SerializeField]
    private float _animationPunchScale;

    [SerializeField]
    private int _punchVibrato;
    
    [SerializeField]
    private RectTransform _iconTransform;
    
    [SerializeField]
    private TMP_Text counterText;

    [SerializeField]
    private ResourceSprites resourceSprites;
    
    [SerializeField]
    private Image iconImage;

    [SerializeField]
    private bool _appearOnFirstDeposit;

    public void Start()
    {
        counterText.text = _count.ToString();
        if (_appearOnFirstDeposit)
            transform.localScale = Vector3.zero;
    }

    public void Initialize(Resource resource)
    {
        iconImage.sprite = resourceSprites.GetResourceSprite(resource);
    }
    
    public void OnChange(int value)
    {
        if (_appearOnFirstDeposit)
        {
            _appearOnFirstDeposit = false;
            transform.DOScale(1, 0.6f).SetEase(Ease.OutBack);
        }

        _count += value;
        if (value > 0)
        {
            _currentDisplayed = _count;
            _iconTransform.localScale = Vector3.one;
            _iconTransform.DOKill();
            _iconTransform.DOPunchScale(Vector3.one * _animationPunchScale, 0.5f, _punchVibrato, 0f);
            counterText.text = _currentDisplayed.ToString();
        }
    }

    private void Update()
    {
        if (_currentDisplayed > _count)
        {
            _currentDisplayed -= 1;
            counterText.text = _currentDisplayed.ToString();
        }
    }
}