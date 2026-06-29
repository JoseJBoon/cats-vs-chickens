using System;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
	void Start()
	{
		if (UnitSelectionManager.Instance != null)
		{
			UnitSelectionManager.Instance.listAllUnits.Add(gameObject);
		}
	}	

	private void OnDestroy()
	{
		if (UnitSelectionManager.Instance != null)
		{
				UnitSelectionManager.Instance.listAllUnits.Remove(gameObject);
		}
	}
}
