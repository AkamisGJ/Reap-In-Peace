using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	public float max_velocity = 15f;
	public float velocity;
	void Start () {
		
	}
	

	void OnTriggerStay(Collider other)
	{
		var playerpos = other.transform.position;
		var desired_velocity = Vector3.Normalize(playerpos - transform.position) * max_velocity;
		transform.Translate(desired_velocity * velocity * Time.deltaTime);
	}
}
