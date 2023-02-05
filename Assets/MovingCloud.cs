using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCloud : MonoBehaviour
{
    [SerializeField]
    private float _minSpeed;

    [SerializeField]
    private float _maxSpeed;

    [SerializeField]
    private float _minX;

    [SerializeField]
    private float _maxX;

    float _moveSpeed;


    // Start is called before the first frame update
    void Start()
    {
        _moveSpeed = Random.Range(_minSpeed, _maxSpeed);
        
        if (Random.value >= 0.5f)
            _moveSpeed = -_moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(_moveSpeed * Time.deltaTime, 0, 0);

        if (transform.position.x < _minX)
            transform.position += new Vector3(_maxX - _minX, 0, 0);
        
        if (transform.position.x > _maxX)
            transform.position -= new Vector3(_maxX - _minX, 0, 0);
    }
}
