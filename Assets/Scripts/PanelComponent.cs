using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class PanelComponent : MonoBehaviour
{
    private bool _isActive;

    private void Start()
    {
        _isActive = gameObject.activeSelf;
    }

    public void SetActive()
    {
        _isActive = !_isActive;
        gameObject.SetActive(_isActive);
    }
}
