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

	private Vector3 prevPosition;
	
	private bool isMoving = false;

	public void MoveToDestination(Vector3 newDestination)
	{
		prevPosition = navMeshAgent.nextPosition;
		isMoving = true;
		animator.SetBool(IsWalkingKey, true);
		navMeshAgent.SetDestination(newDestination);
	}
	void Update()
	{
		Vector3 nextPosition = navMeshAgent.nextPosition;
		if (isMoving == true && navMeshAgent.pathPending == false)
		{
			if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance || nextPosition == prevPosition)
			{
				isMoving = false;
				animator.SetBool(IsWalkingKey, false);
			}
		}
		prevPosition = nextPosition;
	}
}
