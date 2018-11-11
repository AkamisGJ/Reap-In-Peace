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
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            CheckVisibility(other);
            //StartCoroutine("CouroutineCheckVisibility", other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            CheckVisibility(other);
            //StartCoroutine("CouroutineCheckVisibility", other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            CheckVisibility(other);
            //StopCoroutine("CouroutineCheckVisibility");
            UpdateVisibility(other, false);
        }
    }

    //IEnumerator CouroutineCheckVisibility(Collider other)
    //{
    //    while (true)
    //    {
    //        CheckVisibility(other);
    //        yield return new WaitForSeconds(waitTime);
    //    }
    //}

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
