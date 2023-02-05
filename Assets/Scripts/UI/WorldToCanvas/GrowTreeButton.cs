using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GrowTreeButton : W2C
{
    private bool _isShowing;

    public void ButtonPress()
    {
        var waterManager = FindObjectOfType<WaterSystem>();

        HideButton();

        if (waterManager.IsFull == false)
            return;

        var treehouse = FindObjectOfType<TreehouseManager>();
        treehouse.IncreaseTreeHeight();
    }

    void Start()
    {
        transform.localScale = Vector3.zero;
        _extraOffset = new Vector2(0, 100);
    }

    public void ShowButton()
    {
        if (_isShowing)
            return;

        _isShowing = true;
        transform.DOScale(1, 0.8f).SetEase(Ease.OutBack);
    }

    public void HideButton()
    {
        if (_isShowing == false)
            return;

        _isShowing = false;
        transform.DOScale(0, 0.8f).SetEase(Ease.InBack);
    }
}
