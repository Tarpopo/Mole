using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator 
{
    private Transform _transform;
    private Action _onEndRotation;
    private Vector3 _delta;
    private int _frames;
    public bool IsRotating => _frames > 0;
    
    public void StartRotate(Transform transform, Vector3 endRotation,int frames,Action onEndRotation)
    {
        _transform = transform;
        _frames = frames;
        _delta = (endRotation-transform.position) / _frames;
        _onEndRotation = onEndRotation;
    }

    public void RotateUpdate()
    {
        if (_frames <= 0) return;
        _transform.LookAt(_delta);
        _delta += _delta;
        _frames--;
        if (_frames<=0)
        {
            _onEndRotation?.Invoke();
        }
    }

}
