using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility : MonoBehaviour {

    public bool visible;
    public ParticleSystem particleShadow;

	// Use this for initialization
	void Start () {
        visible = false;       
        UpdateVisibilityEffect();
	}

    public void SetVisibility(bool visibility)
    {
        if(visible != visibility)
        {
            visible = visibility;
            UpdateVisibilityEffect();
        }
    }

    public void UpdateVisibilityEffect()
    {
        if (particleShadow != null)
        {
            if (visible)
                particleShadow.Stop();
            else
                if (!particleShadow.isPlaying)
                    particleShadow.Play();
        }
    }
}
