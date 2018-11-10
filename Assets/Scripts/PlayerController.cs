using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    // Keep reference to the camera in order to make movement
    // relative to the current camera view.
    public Camera camera;

    public float forwardSpeed;
    public float turnSpeed;

    // Track the last direction the player moved to. 
    // The model will align itself with this direction progressively
    private Vector3 lastMovementDirection;

    Vector3 getCameraDirection()
    {
        Vector3 cameraDirection = camera.transform.forward;
        Vector3 planarCameraDirection = new Vector3(
            cameraDirection.x,
            0, // Discard the vertical axis
            cameraDirection.z
        );

        return Vector3.Normalize(planarCameraDirection);
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 cameraForward = getCameraDirection();
        Vector3 cameraRight = Vector3.Cross(Vector3.up, cameraForward);
        Vector3 movementDirection = cameraForward * verticalInput + cameraRight * horizontalInput;

        float moveAmount = forwardSpeed * Time.deltaTime;
        Vector3 translation = movementDirection * moveAmount;
        transform.position += translation;

        if (movementDirection.magnitude > 0)
        {
            // Store the last direction of any actual movement done.
            lastMovementDirection = Vector3.Normalize(movementDirection);
        }

        // Slowly make the model orient to the current direction
        // TODO: Adjust with Time.deltaTime
        Vector3 newDirection = Vector3.Slerp(transform.forward, lastMovementDirection, turnSpeed);

        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}