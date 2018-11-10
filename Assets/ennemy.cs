using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ennemy : MonoBehaviour {

	public GameObject PathFinding;
	public int startnode;
	public bool clockwise;

	public int nodetogo;
	private Vector3 target;
	private PathFindingNode[] nodes;
	private NavMeshAgent navMeshAgent;

	// Use this for initialization
	void Start () {
		nodes = PathFinding.GetComponentsInChildren<PathFindingNode>();
		nodetogo = startnode;
		SetTarget();
		navMeshAgent = GetComponent<NavMeshAgent>();
		print(nodes.Length);
	}
	
	// Update is called once per frame
	void Update () {
		navMeshAgent.SetDestination(target);
	}

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		
		if(other.tag == "Node" && other.GetComponent<PathFindingNode>().nodeNumber == nodetogo){
			if(nodetogo < nodes.Length - 1){
				nodetogo++;
				SetTarget();
			}else{
				nodetogo = 0;
				SetTarget();
			}
		}
		print(nodetogo);
	}

	void SetTarget(){
		target = nodes[nodetogo].transform.position;
	}
}
