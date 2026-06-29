using System;	
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
	private static readonly int IsWalkingKey = Animator.StringToHash("isWalking");
	
	[SerializeField]
	private NavMeshAgent navMeshAgent;
	[SerializeField]
	private Animator animator;
	
	private bool isMoving = false;

	public void MoveToDestination(Vector3 newDestination)
	{
		isMoving = true;
		animator.SetBool(IsWalkingKey, true);
		navMeshAgent.SetDestination(newDestination);
	}
	void Update()
	{
		if (isMoving == true && navMeshAgent.pathPending == false)
		{
				if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
				{
						isMoving = false;
						animator.SetBool(IsWalkingKey, false);
				}
		}
			
	}
}
