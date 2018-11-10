using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EyeSetting : MonoBehaviour {

	[Range(0, 7)]
	public float angle;
	public float hauteur;

	[Range(0, 8)]
	public float distance;
	private Mesh mesh;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = new Vector3(angle, hauteur, distance);
	}
}
