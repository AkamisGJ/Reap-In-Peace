using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class DetectPlayer : MonoBehaviour {
	private ennemy parent;
	private NavMeshAgent navMesh;

	// Use this for initialization
	void Start () {
		parent = GetComponentInParent<ennemy>();
		navMesh = GetComponentInParent<NavMeshAgent>();
	}
	

	/// <summary>
	/// OnTriggerStay is called once per frame for every Collider other
	/// that is touching the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerStay(Collider other)
	{
		print("Find Player");
		navMesh.SetDestination(other.transform.position);
		
	}
}
