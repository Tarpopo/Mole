using System;
using UnityEngine;

public class LocalPositionMover
{
    private Transform _transform;
    private Action _onEndMoving;
    private Vector3 _delta;
    private int _frames;
    public bool IsMoving => _frames > 0;
    
    public void StartMoving(Transform transform, Vector3 endPosition,int frames,Action onEndMoving)
    {
        _transform = transform;
        _frames = frames;
        _delta=(endPosition - transform.localPosition) / frames;
        _onEndMoving = onEndMoving;
    }

    public void MovePosition()
    {
        if (_frames <= 0) return;
        _transform.localPosition += _delta;
        _frames--;
        if (_frames<=0)
        {
            _delta=Vector3.zero;
            _onEndMoving?.Invoke();
        }
    }
    

}
