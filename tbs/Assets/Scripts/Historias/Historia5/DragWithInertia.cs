using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragWithInertia : MonoBehaviour
{

    private Vector3 _screenPoint;
    private Vector3 _offset;
    private Vector3 _curScreenPoint;
    private Vector3 _curPosition;

    private Vector3 _velocity;
    private bool _underInertia;
    private float _time = 0.0f;
    public float SmoothTime;


    private void Update()
    {
        if (_underInertia && _time <= SmoothTime)
        {
            transform.position += _velocity;
            _velocity = Vector3.Lerp(_velocity, Vector3.zero, _time);
            _time += Time.smoothDeltaTime;
        }
        else
        {
            _underInertia = false;
            _time = 0.0f;
            transform.position = new Vector3(0, -2.5f, 0);
        }
    }

    void OnMouseDown()
    {
        _screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        _offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        _underInertia = false;
    }

    private void OnMouseDrag()
    {
        Vector3 _prevPosition = _curPosition;
        _curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        _curPosition = Camera.main.ScreenToWorldPoint(_curScreenPoint) + _offset;
        _velocity = _curPosition - _prevPosition;
        transform.position = _curPosition;
    }

    void OnMouseUp()
    {
        _underInertia = true;
        Cursor.visible = true;
    }
}
