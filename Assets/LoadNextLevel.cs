﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextLevel : MonoBehaviour {

	private gamecontrolleur gamecontrolleur;
	public int nextlevel;
	void Start () {
		gamecontrolleur = FindObjectOfType<gamecontrolleur>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		gamecontrolleur.LoadNextLevel();
	}
}
