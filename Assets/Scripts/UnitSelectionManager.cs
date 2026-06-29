using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager Instance {get; set;}

    public List<GameObject> listAllUnits = new List<GameObject>();
    public List<GameObject> listSelectedUnits = new List<GameObject>();
    private Camera mainCamera;
    public LayerMask clickable;
    public LayerMask ground;
    public GameObject groundMarker;
    
	private void Start()
	{
		mainCamera = Camera.main;
	}
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else 
            Instance = this;
    }

  private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

			// check if we are hitting a clickable object
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
			{
				if (Input.GetKey(KeyCode.LeftShift))
					MultiSelect(hit.collider.gameObject);
				else
					SelectByClicking(hit.collider.gameObject);
			}
			else // if we are not hitting a clickable object
			{
				if (!Input.GetKey(KeyCode.LeftShift))
					DeselectAll();
			}
		}

		if (Input.GetMouseButtonUp(1) && listSelectedUnits.Count > 0)
		{
			RaycastHit hit;
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

			// check if we are hitting a clickable object
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
			{
				Vector3 targetPosition = hit.point;

				groundMarker.transform.position = targetPosition + Vector3.up * 0.1f;
				groundMarker.SetActive(false);
				groundMarker.SetActive(true);

				MoveSelectedUnitsTo(targetPosition);
			}
		}
	}

	private void MultiSelect(GameObject unit)
	{
		if (!listSelectedUnits.Contains(unit))
		{
			listSelectedUnits.Add(unit);
			TriggerSelectionIndicator(unit, true);
			// enable unit movement
		}
		else
		{
			// disable unit movement
			TriggerSelectionIndicator(unit, false);
			listSelectedUnits.Remove(unit);
		}
	}

	public void DeselectAll()
	{
		foreach (var unit in listSelectedUnits)
		{
			Debug.Log(unit);
			TriggerSelectionIndicator(unit, false);
			// disable unit movement
			
		}

		groundMarker.SetActive(false);
		listSelectedUnits.Clear();
	}

	private void SelectByClicking(GameObject unit)
	{
		DeselectAll();
		if (listSelectedUnits.Contains(unit))
		{
			// do nothin
		}
		else
		{
			Debug.Log("unit selected: ", unit);
			listSelectedUnits.Add(unit);
			TriggerSelectionIndicator(unit, true);
			// enable unit movement
		}
	}
	private void TriggerSelectionIndicator(GameObject unit, bool isVisible)
	{
		unit.transform.GetChild(0).gameObject.SetActive(isVisible);
	}

	internal void DragSelect(GameObject unit)
	{
		if (!listSelectedUnits.Contains(unit))
		{
			listSelectedUnits.Add(unit);
			TriggerSelectionIndicator(unit,true);
			// enable unit movement
		}
	}
	private Vector3 GetWorldMousePos()
	{
		Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, ground) == true)
		{
			return hitInfo.point;
		}
		return Vector3.zero;
	}
	private void MoveSelectedUnitsTo(Vector3 targetPosition)
	{
		Debug.Log("Moving selected units");
		foreach (GameObject unit in listSelectedUnits)
		{
			UnitMovement movement = unit.GetComponent<UnitMovement>();
			if (movement != null)
			{
					movement.MoveToDestination(targetPosition);
			}
		}
	}
}


