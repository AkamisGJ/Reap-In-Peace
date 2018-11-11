
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class oldboss : MonoBehaviour {
	public float max_velocity = 15f;
	public float velocity;
	public Kill_List kill_List;
	public string name;

	// Update is called once per frame
	void OnTriggerStay(Collider other)
	{
		var playerpos = other.transform.position;
		var desired_velocity = Vector3.Normalize(playerpos - transform.position) * max_velocity;
		transform.Translate(desired_velocity * velocity * Time.deltaTime);
	}

	public void GoingToDie(){
		kill_List.Kill(name);
	}
}