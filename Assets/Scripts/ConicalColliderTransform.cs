using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConicalColliderTransform : MonoBehaviour {

    Light light;

	// Use this for initialization
	void Start () {
        GameObject goLight = transform.parent.gameObject;
        light = goLight.GetComponent<Light>();

        if(light != null && light.type == LightType.Spot)
        {
            float height = light.range;
            float radius = Mathf.Sin((light.spotAngle / 2 * Mathf.PI)/180) * height;

            transform.localScale = new Vector3(
                2 * radius,
                2 * radius,
                height
                );
        }
        
	}

    private void Update()
    {
        float height = light.range;
        float radius = Mathf.Sin((light.spotAngle / 2 * Mathf.PI) / 180) * height;

        transform.localScale = new Vector3(
                2 * radius,
                2 * radius,
                height
                );
    }
}
