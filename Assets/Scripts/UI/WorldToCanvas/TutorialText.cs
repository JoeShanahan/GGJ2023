using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class TutorialText : W2C
{
    [SerializeField] float _appearTime;
    [SerializeField] float _vanishTime;

    [SerializeField] Ease _easeIn;
    [SerializeField] TMP_Text _text;

    public void Init(Vector3 position, string text, Vector2 screenOffset)
    {
        _extraOffset = screenOffset;
        _trackRect.localScale = Vector3.zero;
        _trackRect.DOScale(1, _appearTime).SetEase(_easeIn);
        // _trackRect.localRotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(-25, 25)));

        _text.text = text;
        SetPosition(position);
    }

    public void Dismiss()
    {
        _text.DOFade(0, _vanishTime).OnComplete(() => Destroy(gameObject));
    }
}
