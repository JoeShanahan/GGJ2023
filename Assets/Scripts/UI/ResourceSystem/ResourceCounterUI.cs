using System;
using TMPro;
using UnityEngine;
using DG.Tweening;
using EasyButtons;

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

    public void Start()
    {
        counterText.text = _count.ToString();
    }
    
    public void OnChange(int value)
    {
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