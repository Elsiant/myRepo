using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static public CameraManager _instance;

    public GameObject       _target;
    public float            _moveSpeed;
    private Vector3         _targetPosition;
    public BoxCollider2D    _bound;

    private Vector3         _minBound;
    private Vector3         _maxBound;

    private float           _halfWidth;
    private float           _halfHeight;

    private Camera          _theCamera;

    private void Awake()
    {
        if (null != _instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            _instance = this;
        }
    }
    
    void Start()
    {
        _theCamera  = GetComponent<Camera>();
        _minBound   = _bound.bounds.min;
        _maxBound   = _bound.bounds.max;
        _halfHeight = _theCamera.orthographicSize;
        _halfWidth  = _halfHeight * Screen.width / Screen.height;
    }
    
    void Update()
    {
        if (null != _target.gameObject)
        {
            _targetPosition.Set(_target.transform.position.x, _target.transform.position.y, this.transform.position.z);

            if(20.0f < Vector3.Distance(this.transform.position, _targetPosition))
            {
                this.transform.position = _targetPosition;
            }
            else
            {
                this.transform.position = Vector3.Lerp(this.transform.position, _targetPosition, _moveSpeed * Time.deltaTime); // 1초에 movespeed만큼 이동.
            }

            float clampedX = Mathf.Clamp(this.transform.position.x, _minBound.x + _halfWidth, _maxBound.x - _halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, _minBound.y + _halfHeight, _maxBound.y - _halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
        }
    }

    public void SetBound(BoxCollider2D newBound)
    {
        _bound = newBound;
        _minBound = _bound.bounds.min;
        _maxBound = _bound.bounds.max;
        
    }
}
