using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject visibilityPoint;

    public Vector3 GetVisibilityPoints()
    {
        return visibilityPoint.transform.position;
    }

}
