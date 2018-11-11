using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextLevel : MonoBehaviour {

	private gamecontrolleur gamecontrolleur;
	void Start () {
		gamecontrolleur = FindObjectOfType<gamecontrolleur>();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player"){
			gamecontrolleur.LoadNextLevel();
		}
	}
}
