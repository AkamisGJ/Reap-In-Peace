using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingLight : MonoBehaviour {

    [Range(0, 90)]
    public float maxAngle;
    [Range(0, 90)]
    public float clampAngle;
    public Vector3 axeOfRotation;
    [Range(0,10)]
    public float swingingSpeed;

    //Vector3 positionOfRotation;

    // Use this for initialization
    void Start () {
        //positionOfRotation = gameObject.transform.parent.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 rotation = axeOfRotation * Mathf.Clamp(maxAngle * Mathf.Sin(Time.time * swingingSpeed), -clampAngle, clampAngle);
        gameObject.transform.parent.transform.localRotation = Quaternion.Euler(rotation);
    }
}
