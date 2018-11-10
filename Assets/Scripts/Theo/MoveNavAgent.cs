using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveNavAgent : MonoBehaviour {

	private NavMeshAgent navMeshAgent;
	public Transform target;

	void Start () {
		navMeshAgent = GetComponent<NavMeshAgent>();
	}
	
	void Update () {
		navMeshAgent.SetDestination(target.position);
	}
}
