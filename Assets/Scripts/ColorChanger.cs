using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger
{
    private Material _material;
    private Color _deltaColor;
    private int _frames;
    private int _currentFrames;
    private bool _isReturnBack;
    private Color _startColor;
    private Action _endAction;
    public bool IsColorChanging => _currentFrames > 0;
    
    public void StartChangeColor(Material material, Color target, int frames,Action endAction ,bool returnBack = false)
    {
        _material = material;
        _startColor = material.color;
        _deltaColor = (target - material.color) / frames;
        _frames = frames;
        _currentFrames = frames;
        _isReturnBack = returnBack;
        _endAction = endAction;
    }
    
    public void ChangeColorUpdate()
    {
        if (_currentFrames <= 0) return;
        var tempColor = _material.color;
        tempColor += _deltaColor;
        _material.color = tempColor;
        _currentFrames--;
        if (_currentFrames > 0) return;
        if (_isReturnBack)
        {
            _currentFrames = _frames;
            _deltaColor = (_startColor - _material.color) / _frames;
            _isReturnBack = false;
            return;
        }
        _endAction?.Invoke();
    }
}
