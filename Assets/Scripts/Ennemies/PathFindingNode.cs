using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingNode : MonoBehaviour {

	[Header ("Gizmos")]
	public Color color;
	public float radius = 0.5f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () { }

	/// <summary>
	/// Callback to draw gizmos that are pickable and always drawn.
	/// </summary>
	void OnDrawGizmos () {
		Gizmos.color = color;
		Gizmos.DrawSphere (transform.position, radius);
	}
}