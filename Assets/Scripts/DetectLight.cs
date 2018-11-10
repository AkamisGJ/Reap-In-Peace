using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectLight : MonoBehaviour {

    ConicalColliderTransform cone;

    float waitTime = 0.01f;

    private void Awake()
    {
        cone = transform.parent.gameObject.GetComponent<ConicalColliderTransform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //SetVisibility(other, true);
        StartCoroutine("CouroutineCheckVisibility", other);
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    CheckVisibility(other);
    //}

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine("CouroutineCheckVisibility");
        SetVisibility(other, false);
    }

    private void SetVisibility(Collider other, bool visibility)
    {
        Visibility vComp = other.GetComponent<Visibility>();

        if (vComp != null)
        {
            vComp.SetVisibility(visibility);
        }
    }

    private void CheckVisibility(Collider other)
    {
        Visibility vComp = other.GetComponent<Visibility>();

        if (vComp != null)
        {
            Vector3 pos = vComp.GetVisibilityPoints();
            Vector3 origin = cone.transform.position;

            RaycastHit hit;

            bool visibility = false;

            float maxDistance = Mathf.Clamp((pos - origin).magnitude, 0, cone.getHeight());

            Debug.DrawRay(origin, pos - origin, Color.magenta);

            if (Physics.Raycast(origin, pos - origin, out hit, maxDistance, LayerMask.NameToLayer("Environnement")))
            {
                visibility = false;
            }
            else
            {
                visibility = true;
            }
            //else
            //{
            //    visibility = false;
            //}

            vComp.SetVisibility(visibility);
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
