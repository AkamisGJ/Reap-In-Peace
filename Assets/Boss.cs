using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    public string name;
	public float max_velocity = 15f;
<<<<<<< HEAD
	public float velocity;
	public Transform[] pathfinding;
	public string name;
	
=======
>>>>>>> 2164740c9cb948107db838a5b7fb1d9b308bba28

    //public float velocity;
    public float cMoveAwayPlayer = 1;
    private bool bMoveAwayPlayer = false;

    public float cMoveAwayWall = 1;
    private bool bMoveAwayWall = false;
    public float wallDistance = 5;

    public float gravity = 10;


    public Transform[] pathfinding;

    public Kill_List killList;

    private Vector3 velocity;
    private Vector3 playerPosition;
    private Rigidbody rb;

    private void Start()
    {
        velocity = Vector3.zero;
        playerPosition = Vector3.zero;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Gravity();

        velocity = Vector3.zero;

        RepulseFromObstacle();

        if (bMoveAwayPlayer)
            MoveAway(playerPosition, cMoveAwayPlayer);

        if (velocity.magnitude > max_velocity)
            velocity = velocity * max_velocity / velocity.magnitude;

        
        //rb.velocity = velocity;
        transform.position += velocity * Time.deltaTime;

        transform.rotation = Quaternion.FromToRotation(- Vector3.forward, new Vector3(velocity.x, velocity.y, 0));

        bMoveAwayPlayer = false;
    }

    void OnTriggerStay(Collider other)
	{
        //Vector3 farest = Vector3.zero;

        //for (int i = 0; i < pathfinding.Length; i++)
        //{
        //    var node = pathfinding[i];
        //    var distanceBoss = (transform.position + node.position).sqrMagnitude;
        //    Debug.DrawLine(transform.position, node.position);
        //    print(distanceBoss + node.name);

        //    if (distanceBoss > farest.sqrMagnitude)
        //    {
        //        farest = node.position;
        //    }
        //}
        //print("Farest = " + farest);

        if (other.CompareTag("Player"))
        {
            bMoveAwayPlayer = true;
            playerPosition = other.transform.position;
        }
    }

    private void MoveAway(Vector3 pos, float constant)
    {
        //velocity + = new Vector3(
        //    constant / (
        //    0
        //    );
        velocity += (transform.position - pos).normalized * constant;
    }

    private void RepulseFromObstacle()
    {
        RaycastHit hit;

    }

    private void Gravity()
    {
        //velocity += ;
        rb.AddForce(Vector3.down * gravity * Time.deltaTime);
    }

    public void Kill()
    {
        if (killList != null)
            killList.Kill(name);

        Destroy(gameObject);
    }

}
