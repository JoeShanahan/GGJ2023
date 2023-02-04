using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class TextIconBurst : W2C
{
        [SerializeField] Vector2 _driftAmount;
        [SerializeField] RectTransform _rect;
        
        [Header("Timings")]
        [SerializeField] float _scaleTime;
        [SerializeField] float _aliveTime;
        [SerializeField] float _fadeTime;

        [Space(8)]
        [SerializeField] TMP_Text _text;
        [SerializeField] Image _image;

        void Start()
        {
            _rect.localScale = Vector3.zero;
            _rect.DOScale(Vector3.one, _scaleTime);

            _rect.DOAnchorPos(_rect.anchoredPosition + _driftAmount, _aliveTime)
                 .SetEase(Ease.Linear);

            _canvasGroup.DOFade(0, _fadeTime)
                        .SetDelay(_aliveTime - _fadeTime)
                        .SetEase(Ease.Linear);

            Destroy(gameObject, _aliveTime + 0.5f);           
        }

        public void SetData(Vector3 worldPos, string text, Sprite icon)
        {
            SetPosition(worldPos);
            _text.text = text;
            _image.sprite = icon;
        }

}
