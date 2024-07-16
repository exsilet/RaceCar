using System;
using DefaultNamespace;
using UnityEngine;

public class DragCar : MonoBehaviour
{
    private bool _dragging;
    private Vector3 _currentPosition;
    private Vector3 _offset;

    private GarageSlot _slot;

    private void Awake()
    {
        _currentPosition = transform.position;
    }

    private void Update()
    {
        if(!_dragging)return;
        
        // var mousePosition = GetMousePos();
        //
        // transform.position = mousePosition;
    }

    public void Init(GarageSlot slot)
    {
        _slot = slot;
    }

    private void OnMouseDown()
    {
        _dragging = true;
        _offset = Input.mousePosition - GetMousePos();
    }
    
    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - _offset);
    }

    private void OnMouseUp()
    {
        transform.position = _currentPosition;
        _dragging = false;
    }

    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }
}
