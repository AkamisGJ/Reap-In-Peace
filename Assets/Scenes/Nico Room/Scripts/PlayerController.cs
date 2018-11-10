using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float forwardSpeed;
    public float turnSpeed;

    // Track the last direction the player moved to. 
    // The model will align itself with this direction progressively
    private Vector3 movementDirection;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        movementDirection = new Vector3(horizontalInput, 0, verticalInput);

        float moveAmount = forwardSpeed * Time.deltaTime;
        transform.position = transform.position + (movementDirection * moveAmount);

        // Slowly make the model orient to the current direction
        // TODO: Adjust with Time.deltaTime
        Vector3 newDirection = Vector3.Slerp(transform.forward, movementDirection, turnSpeed);

        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}