using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectLight : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Visibility v = other.GetComponent<Visibility>();

            if(v != null)
            {
                v.SetVisibility(true);
            }            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Visibility v = other.GetComponent<Visibility>();

            if (v != null)
            {
                v.SetVisibility(false);
            }
        }
    }
}
