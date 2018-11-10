using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float forwardSpeed;
    public float turnSpeed;

    void Update()
    {
        float turnInput = Input.GetAxis("Horizontal");
        float turnAmount = turnInput * turnSpeed * Time.deltaTime;
        transform.RotateAround(transform.position, Vector3.up, turnAmount);

        float forwardInput = Input.GetAxis("Vertical");
        float moveAmount = forwardInput * forwardSpeed * Time.deltaTime;

        Vector3 direction = transform.forward;
        transform.Translate(Vector3.forward * moveAmount);
    }
}