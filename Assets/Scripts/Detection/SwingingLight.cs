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

    Quaternion initRotation;

    // Use this for initialization
    void Start () {
        initRotation = transform.localRotation;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 rotation = axeOfRotation * Mathf.Clamp(maxAngle * Mathf.Sin(Time.time * swingingSpeed), -clampAngle, clampAngle);
        transform.localRotation = initRotation * Quaternion.Euler(rotation);
    }
}
