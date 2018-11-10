using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ennemy : MonoBehaviour {

	public GameObject PathFinding;
	public bool backAndForth = true;
	public bool findplayer = false;

	// Index of the next node
	private int nodetogo = 0;
	// +1 when going over nodes in order. -1 when going in reverse order
	private int step = +1;

	private Vector3 target;
	private PathFindingNode[] nodes;
	private NavMeshAgent navMeshAgent;

	// Use this for initialization
	void Start () {
		nodes = PathFinding.GetComponentsInChildren<PathFindingNode> ();
		SetTarget ();
		navMeshAgent = GetComponent<NavMeshAgent> ();
		print (nodes.Length);
	}

	// Update is called once per frame
	void Update () {
		if (findplayer == false) {
			navMeshAgent.SetDestination (target);
		}
	}

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter (Collider other) {
		// Ignore encountering nodes if chasing the player
		if (findplayer) return;

		if (other.tag != "Node") return;

		Collider target = nodes[nodetogo].GetComponent<Collider> ();

		if (other == target) {
			// Switch to next path node
			int next = nodetogo + step;
			if (next == -1 || next == nodes.Length) {
				// We found the last node.
				if (backAndForth) {
					step = -step;
					next = nodetogo + step;
				} else {
					next = 0;
				}
			}

			nodetogo = next;
			SetTarget ();
		}
	}

	void SetTarget () {
		target = nodes[nodetogo].transform.position;
	}
}