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
	[SerializeField] private float arrivalTolerance = 0.25f;
    [SerializeField] private float stoppedSpeed = 0.05f;

	private Vector3 initialPosition;
	
	private bool isMoving = false;

	public void MoveToDestination(Vector3 newDestination)
	{
		initialPosition = navMeshAgent.nextPosition;
		isMoving = true;
		animator.SetBool(IsWalkingKey, true);
		navMeshAgent.isStopped = false;
		navMeshAgent.SetDestination(newDestination);
	}
	void Update()
	{
		if (isMoving == false ||
			navMeshAgent.pathPending == true ||
			float.IsInfinity(navMeshAgent.remainingDistance) == true)
		{
			return;
		}

        float threshold = Mathf.Max(navMeshAgent.stoppingDistance, 0.01f) + arrivalTolerance;
        bool closeEnough = navMeshAgent.remainingDistance <= threshold;
        bool basicallyStopped = navMeshAgent.velocity.sqrMagnitude <= stoppedSpeed * stoppedSpeed;
        bool noPathLeft = !navMeshAgent.hasPath;

        if (closeEnough == true && (basicallyStopped == true || noPathLeft == true))
        {
            isMoving = false;
            animator.SetBool(IsWalkingKey, false);
            navMeshAgent.isStopped = true;
        }
	}
}
