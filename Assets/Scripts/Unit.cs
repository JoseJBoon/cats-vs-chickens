using System;
using UnityEngine;

public class Unit : MonoBehaviour
{

  void Start()
	{
		UnitSelectionManager.Instance.listAllUnits.Add(gameObject);
	}
	private void OnDestroy()
	{
		UnitSelectionManager.Instance.listAllUnits.Remove(gameObject);
	}

	
}
