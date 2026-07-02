using System;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform _mainCamera;
    private Transform _transform;

    private void Awake()
    {
        _mainCamera = Camera.main.transform;
        _transform = transform;
    }

    private void LateUpdate()
    {
        _transform.rotation = _mainCamera.rotation;
    }
}
