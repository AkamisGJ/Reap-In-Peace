using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kill_List : MonoBehaviour {

	public Image monsanto_kill;
	public Image bolore_kill;
	public Image trump_kill;
	public Image win;
	
	public void Kill(string name){
		switch(name){
			case "monsanto":
				monsanto_kill.enabled = true;
			break;

			case "bolore":
				bolore_kill.enabled = true;
			break;

			case "trump":
				trump_kill.enabled = true;
				win.enabled = true;
			break;
		}
	}
}
