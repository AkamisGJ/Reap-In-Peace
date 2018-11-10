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
    private Vector3 movementDirection;

    Vector3 getCameraDirection()
    {
        Vector3 cameraDirection = camera.transform.forward;
        Vector3 planarCameraDirection = Vector3.Normalize(
            new Vector3(
                cameraDirection.x,
                0, // Discard the vertical axis
                cameraDirection.z
            )
        );

        return planarCameraDirection;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 cameraForward = getCameraDirection();
        Vector3 cameraRight = Vector3.Cross(Vector3.up, cameraForward);
        movementDirection = cameraForward * verticalInput + cameraRight * horizontalInput;

        float moveAmount = forwardSpeed * Time.deltaTime;
        transform.position = transform.position + (movementDirection * moveAmount);

        // Slowly make the model orient to the current direction
        // TODO: Adjust with Time.deltaTime
        Vector3 newDirection = Vector3.Slerp(transform.forward, movementDirection, turnSpeed);

        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}