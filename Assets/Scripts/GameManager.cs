using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	[SerializeField]
	private List<Unit> allUnits;
	[SerializeField]
	private LayerMask groundLayer;

	private void Start()
	{
		instance = this;
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
		Vector3 position = Vector3.zero;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, groundLayer) == true)
		{
			position = hitInfo.point;
		}
		return position;
	}
}
