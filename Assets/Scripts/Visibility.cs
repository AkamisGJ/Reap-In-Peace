using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility : MonoBehaviour {

    bool visible;
    MeshRenderer meshRenderer;

	// Use this for initialization
	void Start () {
        visible = false;
        meshRenderer = GetComponent<MeshRenderer>();
	}

    public void SetVisibility(bool visibility)
    {
        visible = visibility;

        if (visible)
        {
            meshRenderer.material.SetColor("_Color", Color.red);
        }
        else
        {
            meshRenderer.material.SetColor("_Color", Color.white);
        }
    }
}
