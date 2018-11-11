using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectLight : MonoBehaviour {

    private Ennemy ennemy;

    ConicalColliderTransform cone;
    bool visible;

    private void Awake()
    {
        cone = transform.parent.gameObject.GetComponent<ConicalColliderTransform>();
        ennemy = transform.parent.parent.parent.parent.gameObject.GetComponent<Ennemy>();

        if (ennemy == null)
            Debug.Log("ERROR : no ennemy in Detect Light");
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            CheckVisibility(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            CheckVisibility(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            CheckVisibility(other);
            UpdateVisibility(other, false);
        }
    }

    private void CheckVisibility(Collider other)
    {
        Player player = other.GetComponent<Player>();
        

        if (player != null)
        {
            Vector3 pos = player.GetVisibilityPoints();
            Vector3 origin = cone.transform.position;

            RaycastHit hit;
            float maxDistance = Mathf.Clamp((pos - origin).magnitude, 0, cone.getHeight());

            Debug.DrawRay(origin, pos - origin, Color.magenta);
            //bool newVisibility = ! Physics.Raycast(origin, pos - origin, out hit, maxDistance, LayerMask.NameToLayer("Environnement"));
            int mask = 1 << LayerMask.NameToLayer("Environnement");
            bool newVisibility = ! Physics.Raycast(origin, pos - origin, out hit, maxDistance, mask);

            UpdateVisibility(other, newVisibility);
        }
    }

    private void UpdateVisibility(Collider other, bool newVisibility)
    {
        SetEnnemyVisibility(newVisibility);
        visible = newVisibility;

        Visibility visibilityScript = other.GetComponent<Visibility>();
        if (visibilityScript != null)
            visibilityScript.SetVisibility(newVisibility);
    }

    public void SetEnnemyVisibility(bool newVisibility)
    {
        if (ennemy != null && visible != newVisibility)
        {
            if (newVisibility)
            {
                ennemy.PlayerEnterFieldOfVision();
                Debug.Log("JE T'AI VU!");
            } 
            else
            {
                ennemy.PlayerExitFieldOfVision();
                Debug.Log("Mais t'es ou?");
            }    
        }
    }
}
