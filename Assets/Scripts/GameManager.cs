using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	[SerializeField]
	private List<Unit> allUnits;
	[SerializeField]
	private LayerMask groundLayer;

	private Camera _mainCamera;

	private void Awake()
	{
		_mainCamera = Camera.main;
	}

	private void Start()
	{
		Instance = this;
	}
	public void RegisterUnit(Unit newUnit)
	{
		allUnits.Add(newUnit);
	}

	public void UnregisterUnit(Unit unit)
	{
		allUnits.Remove(unit);
	}

	private void Update()
	{
		if (Input.GetMouseButtonUp(1) == true)
		{
			for (int i = 0; i < allUnits.Count; i++)
			{
				allUnits[i].MoveToDestination(GetWorldMousePos());
			}
		}
	}

	private Vector3 GetWorldMousePos()
	{
		Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, groundLayer) == true)
		{
			return hitInfo.point;
		}
		return Vector3.zero;
	}
}
