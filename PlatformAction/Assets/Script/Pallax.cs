using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pallax : MonoBehaviour
{
    private float       _length;
    private float       _startPos;
    public GameObject  _cam;
    public float        _parallaxEffect;
    
    void Start()
    {
        _startPos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    
    void FixedUpdate()
    {
        float temp = (_cam.transform.position.x * (1.0f - _parallaxEffect));
        float dist = (_cam.transform.position.x * _parallaxEffect);
        float height = (_cam.transform.position.y * _parallaxEffect);

        transform.position = new Vector3(_startPos + dist, height, transform.position.z);

        if (temp > _startPos + _length)
        {
            _startPos += (2 * _length);
        }
        else if (temp < _startPos - _length) 
        {
            _startPos -= (2 * _length);
        }
    }
}
