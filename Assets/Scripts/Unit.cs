using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
	[SerializeField]
	private NavMeshAgent navMeshAgent;
	[SerializeField]
	private Animator animator;

	private bool isMoving = false;
	[SerializeField, Range(0.0f, 5.0f)]
	private float stoppingDistance = 1f;
	private void Start()
	{
		GameManager.instance.RegisterUnit(this);
	}

	private void Update()
	{
		if (isMoving == true && navMeshAgent.pathPending == false)
		{
			if (navMeshAgent.remainingDistance <= stoppingDistance)
			{
				isMoving = false;
				animator.SetBool("isWalking", false);
			}
		}
	}
	public void MoveToDestination(Vector3 newDestination)
	{
		isMoving = true;
		animator.SetBool("isWalking", true);
		navMeshAgent.SetDestination(newDestination);
	}
}
