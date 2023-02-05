using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleScreenLogo : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _sprite;

    [SerializeField]
    private float _wobbleSpeed;

    [SerializeField]
    private float _wobbleAmount;

    private Vector3 _origin;

    private bool _killed;

    // Start is called before the first frame update
    void Start()
    {
        _origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_killed)
            return;

        Vector3 offset = new Vector3(0, Mathf.Sin(Time.time * _wobbleSpeed) * _wobbleAmount);
        transform.position = _origin + offset;

        if (Input.GetMouseButtonDown(0))
        {
            ProgressionManager.CompleteStep(ProgressStep.DismissedTitle);
            _sprite.DOFade(0, 2).OnComplete(() => Destroy(gameObject));
            _killed = true;
        }
    }
}
