using System;
using UnityEngine;

[RequireComponent(typeof(SpaceChecker))]
public class Placement : MonoBehaviour
{
    public delegate void OnPlacementHandler();
    
    [SerializeField] private LayerMask cursorLayerMask;
    [SerializeField] private BoxHighlight boxHighLight;
    
    private Transform _cursor;
    private Camera _mainCamera;
    private SpaceChecker _spaceChecker;
    private Transform _buildingPrefab;

    public event OnPlacementHandler OnBuildingPlaced;
    public event OnPlacementHandler OnBuildingCancel;
    
    private void Start()
    {
        _cursor = transform;
        _mainCamera = Camera.main;
        _spaceChecker = GetComponent<SpaceChecker>();
    }

    private void Update()
    {
        TrackMouseMovement();
        MouseInteraction();
    }

    private void MouseInteraction()
    {
        if (!_buildingPrefab)
            return;
        
        if (Input.GetMouseButtonDown(0))
        {
            if(_spaceChecker.IsFreeSpace)
                PlaceBuilding();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            CancelBuilding();
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
        _cursor.position = SnapPosition(hitInfo.point);
    }

    private static Vector3 SnapPosition(Vector3 position)
    {
        position.x = Mathf.Floor(position.x) + .5f;
        position.z = Mathf.Floor(position.z) + .5f;
        return position;
    }

    public void AssignBuilding(Transform building)
    {
        boxHighLight.gameObject.SetActive(true);
        boxHighLight.Resize(building.GetComponentInChildren<MeshRenderer>().bounds);
        _buildingPrefab = building;
    }

    private void PlaceBuilding()
    {
        var instance = Instantiate(_buildingPrefab);
        instance.position = SnapPosition(_cursor.position);
        _buildingPrefab = null;
        boxHighLight.gameObject.SetActive(false);
        OnBuildingPlaced?.Invoke();
    }

    private void CancelBuilding()
    {
        boxHighLight.gameObject.SetActive(false);
        _buildingPrefab = null;
        OnBuildingCancel?.Invoke();
    }
}
