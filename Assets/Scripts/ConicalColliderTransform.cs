using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConicalColliderTransform : MonoBehaviour {

    Light spotLight;
    float angle;
    float height;

	// Use this for initialization
	void Awake () {
        GameObject goLight = transform.parent.gameObject;
        spotLight = goLight.GetComponent<Light>();  
	}

    private void Update()
    {
        if (spotLight != null && spotLight.type == LightType.Spot)
        {
            angle = (spotLight.spotAngle / 2 * Mathf.PI) / 180;
            height = spotLight.range;
            float radius = Mathf.Sin(angle) * height;

            transform.localScale = new Vector3(
                2 * radius,
                2 * radius,
                height
                );
        }
    }

    public float getHeight()
    {
        return height;
    }

    public float getAngle()
    {
        return angle;
    }
}
