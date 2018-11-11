using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	public float max_velocity = 15f;
	public float velocity;
	public Transform[] pathfinding;
	

	void OnTriggerStay(Collider other)
	{
		Vector3 farest = Vector3.zero;

		for (int i = 0; i < pathfinding.Length; i++)
		{
			var node = pathfinding[i];
			var distanceBoss = (transform.position + node.position).sqrMagnitude;
			Debug.DrawLine(transform.position, node.position);
			print(distanceBoss + node.name);

			if(distanceBoss > farest.sqrMagnitude){
				farest = node.position;
			}
		}
		print("Farest = " + farest);

	}
}
