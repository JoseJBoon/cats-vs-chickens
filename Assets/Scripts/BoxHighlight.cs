using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class BoxHighlight : MonoBehaviour
{
    [SerializeField] private SpaceChecker spaceChecker;
    [SerializeField] private Material availableSpace;
    [SerializeField] private Material occupiedSpace;

    private Transform _transform;
    private MeshRenderer _meshRenderer;
    private bool _prevIsFreeSpace;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material = availableSpace;
        _transform = transform;
    }

    private void Start()
    {
        _prevIsFreeSpace = spaceChecker.IsFreeSpace;
    }
    
    private void Update()
    {
        if (_prevIsFreeSpace == spaceChecker.IsFreeSpace)
            return;
        
        _meshRenderer.material = spaceChecker.IsFreeSpace ? availableSpace : occupiedSpace;
        _prevIsFreeSpace = spaceChecker.IsFreeSpace;
    }

    public void Resize(Bounds bounds)
    {
        _transform.localScale = bounds.size;
        
        Vector3 localPosition = _transform.localPosition;
        localPosition.y = bounds.extents.y;
        _transform.localPosition = localPosition;
    }
    
}
