using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float forwardSpeed;
    public float turnSpeed;
    public float gravity;

    // Track the last direction the player moved to. 
    // The model will align itself with this direction progressively
    private Vector3 lastMovementDirection;
    private CharacterController _controller;

    Animator anim;
    int attackHash = Animator.StringToHash ("Attack");
    int attackAnimationHash = Animator.StringToHash ("CoupDeFaux");
    private bool attack = false;

    void Start () {
        _controller = GetComponent<CharacterController> ();
        anim = GetComponent<Animator> ();
    }

    // Used to to make movement
    // relative to the current camera view.
    Vector3 getCameraDirection () {
        Camera camera = Camera.main;
        Vector3 cameraDirection = camera.transform.forward;
        Vector3 planarCameraDirection = new Vector3 (
            cameraDirection.x,
            0, // Discard the vertical axis
            cameraDirection.z
        );

        return Vector3.Normalize (planarCameraDirection);
    }

    void Update () {
        handleMovement ();
        handleAttack ();
    }

    void handleMovement () {
        float horizontalInput = Input.GetAxis ("Horizontal");
        float verticalInput = Input.GetAxis ("Vertical");
        Vector3 cameraForward = getCameraDirection ();
        Vector3 cameraRight = Vector3.Cross (Vector3.up, cameraForward);
        Vector3 movementDirection = cameraForward * verticalInput + cameraRight * horizontalInput;

        float moveAmount = forwardSpeed * Time.deltaTime;
        _controller.Move (movementDirection * moveAmount - (Vector3.up * gravity * Time.deltaTime));



        if (movementDirection.magnitude > 0) {
            // Store the last direction of any actual movement done.
            lastMovementDirection = Vector3.Normalize (movementDirection);
        }

        // Slowly make the model orient to the current direction
        // TODO: Adjust with Time.deltaTime
        Vector3 newDirection = Vector3.Slerp (transform.forward, lastMovementDirection, turnSpeed);

        transform.rotation = Quaternion.LookRotation (newDirection);
    }

    void handleAttack () {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo (0);
        bool attacked = Input.GetButton ("Fire1") || Input.GetKey (KeyCode.Space);
        bool isAttacking = stateInfo.IsTag ("CoupDeFaux");

        if (attacked && !isAttacking) {
            anim.SetTrigger (attackHash);
            GetComponent<AudioSource>().PlayDelayed(0.30f);
        }
    }
    public void ToogleAttack(){
        attack = !attack;
    }

    void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Boss" && attack == true){
            
            Destroy(other.gameObject);
        }
    }

    

}