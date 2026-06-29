using System;
using UnityEngine;

public class Placement : MonoBehaviour
{
    [SerializeField] private Transform cursor;
    [SerializeField] private LayerMask cursorLayerMask;
    
    private Camera _mainCamera;
    private SpaceChecker _spaceChecker;
    private Transform _building;
    
    private void Start()
    {
        _mainCamera = Camera.main;
        _spaceChecker = GetComponentInChildren<SpaceChecker>();
    }

    private void Update()
    {
        TrackMouseMovement();
        MouseInteraction();
    }

    private void MouseInteraction()
    {
        if (!_building)
            return;
        
        if (Input.GetMouseButtonDown(0) && _spaceChecker.IsFreeSpace)
        {
            _building.SetParent(null);
            _building.position = SnapPosition(_building.position);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Cancel!!!");
            // Clear selection (give back to UI)
        }
    }

    private void TrackMouseMovement()
    {
        Vector2 mousePosition = Input.mousePosition;
        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hitInfo, 100.0f, cursorLayerMask))
        {
            return;
        }
        cursor.position = SnapPosition(hitInfo.point);
    }

    private Vector3 SnapPosition(Vector3 position)
    {
        position.x = Mathf.Floor(position.x) + .5f;
        position.z = Mathf.Floor(position.z) + .5f;
        return position;
    }

    public void AssignBuilding(Transform building)
    {
        _building = building;
        cursor.gameObject.SetActive(true);
    }

    public void CancelBuilding()
    {
        cursor.gameObject.SetActive(false);
        // return building
    }
}
