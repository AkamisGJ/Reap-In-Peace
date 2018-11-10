using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingLight : MonoBehaviour {

    [Range(0, 90)]
    public float maxAngle;
    public Vector3 axeOfRotation;
    [Range(0,10)]
    public float swingingSpeed;

    

    Vector3 positionOfRotation;
    Light spotLight;

    // Use this for initialization
    void Start () {
        spotLight = GetComponent<Light>();
        positionOfRotation = gameObject.transform.parent.transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        //spotLight.transform.RotateAround(positionOfRotation, axeOfRotation, Mathf.Cos(Time.time));
        Vector3 rotation = axeOfRotation * maxAngle * Mathf.Sin(Time.time * swingingSpeed);
        gameObject.transform.parent.transform.localRotation = Quaternion.Euler(rotation);
    }
}
