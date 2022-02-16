using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalScaleChanger
{
    private Transform _transform;
    private Vector3 _startScale;
    private Vector3 _delta;
    private int _frames;
    private int _currentFrames;
    private bool _isReturnBack;
    public bool IsScaling => _currentFrames > 0;
    
    public void StartScaling(Transform transform, Vector3 endScale,int frames, bool returnBack=false)
    {
        _transform = transform;
        _frames = frames;
        _currentFrames = _frames;
        _startScale = _transform.localScale;
        _isReturnBack = returnBack;
        _delta=(endScale - transform.localScale) / frames;
    }

    public void UpdateScale()
    {
        if (_currentFrames <= 0) return;
        _transform.localScale += _delta;
        _currentFrames--;
        
        if (_currentFrames > 0) return;
        if (_isReturnBack)
        {
            _currentFrames = _frames;
            _delta = (_startScale - _transform.localScale) / _frames;
            _isReturnBack = false;
        }
    }
}
