using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectLight : MonoBehaviour {

    public Ennemy ennemy;

    ConicalColliderTransform cone;
    float waitTime = 0.01f;
    bool visible;

    private void Awake()
    {
        cone = transform.parent.gameObject.GetComponent<ConicalColliderTransform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine("CouroutineCheckVisibility", other);
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine("CouroutineCheckVisibility");

        Player player = other.GetComponent<Player>();
        if (player != null)
            UpdateVisibility(other, false);
    }

    private void UpdateVisibility(Collider other, bool newVisibility)
    {
        SetEnnemyVisibility(newVisibility);
        visible = newVisibility;

        Visibility visibilityScript = other.GetComponent<Visibility>();
        if (visibilityScript != null)
            visibilityScript.SetVisibility(newVisibility);
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
            bool newVisibility = ! Physics.Raycast(origin, pos - origin, out hit, maxDistance, LayerMask.NameToLayer("Environnement"));


            UpdateVisibility(other, newVisibility);
        }
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
                ennemy.PlayerExitFieldOfVision();
        }
    }

    IEnumerator CouroutineCheckVisibility(Collider other)
    {
        while (true)
        {
            CheckVisibility(other);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
