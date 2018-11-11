using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ennemy : MonoBehaviour {

    
    public float proximityDistance = 2f;
    public float alertDistance = 10f;

    //Nav Mesh parameters
    public GameObject PathFinding;
    public bool backAndForth = true;    // Pour faire des aller retours, ou bien des rondes.
    private NavMeshAgent navMeshAgent;
    private PathFindingNode[] nodes;   
    private int nodetogo = 0;   // Index of the next node
    public int step = +1;   // +1 when going over nodes in order. -1 when going in reverse order

    //Targets
    private Vector3 navMeshTarget = Vector3.zero;
    private Vector3 alertTarget = Vector3.zero;
    private Vector3 playerTarget = Vector3.zero;

    private EnnemyStateMachine stateMachine;
    private Player player;

    // Use this for initialization
    void Start () {
        stateMachine = new EnnemyStateMachine ();

        GameObject goPlayer = GameObject.FindGameObjectWithTag("Player");
        if (goPlayer != null)
            player = goPlayer.GetComponent<Player>();

        if (player == null)
            Debug.Log("ERROR : no player ref in Ennemy");

        //Init Nav Mesh Agent
        nodes = PathFinding.GetComponentsInChildren<PathFindingNode> ();
        navMeshAgent = GetComponent<NavMeshAgent> ();
        SetNavMeshTarget ();
        Debug.Log("Number of Nodes: " + nodes.Length);
    }

    // Update is called once per frame
    void Update () {
        UpdateBehaviorFromStateMachine ();
    }

    private void UpdateBehaviorFromStateMachine () {
        switch (stateMachine.currentState) {
            case State.Win:
                Idle ();
                break;
            case State.Patrolling:
                Patrol ();
                break;
            case State.Alerted:
                Alert ();
                break;
            case State.Seeking:
                Seek ();
                break;
            case State.Dead:
                Die ();
                break;
        }
    }

    //EVENTS
    //OUT
    private void AlertOthers () {
        //TODO
        //Get Near enemies
        //For each near ennemis - alert

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Ennemy");
        foreach(GameObject go in gameObjects)
        {
            Ennemy ennemy = go.GetComponent<Ennemy>();
            if(ennemy != null)
            {
                Vector3 direction = ennemy.transform.position - transform.position;
                if (direction.magnitude < alertDistance)
                {
                    int mask = 1 << LayerMask.NameToLayer("Environnement");
                    Debug.DrawRay(transform.position, direction, Color.red);
                    RaycastHit hit;
                    bool inSight = !Physics.Raycast(transform.position, direction, out hit, direction.magnitude, mask);
                    if (inSight)
                        ennemy.Alert(playerTarget);
                }
            }
        }
    }

    //IN
    public void Alert (Vector3 position) {
        alertTarget = position;
        stateMachine.MoveNext (Command.Alert);
    }

    public void Kill () {
        stateMachine.MoveNext (Command.Die);
    }

    public void PlayerEnterFieldOfVision () {
        stateMachine.MoveNext (Command.Seek);
    }

    public void PlayerExitFieldOfVision () {
        alertTarget = playerTarget;
        stateMachine.MoveNext (Command.LoseTrack);
    }

    //IN - TRIGGER EVENT
    private void OnTriggerEnter (Collider other) {
        Collider targetNodes = nodes[nodetogo].GetComponent<Collider> ();

        if (other == targetNodes) {
            UpdateNextNavMeshNode ();
            SetNavMeshTarget();
        }
    }

    //BEHAVIORS
    private void Idle () {
        navMeshAgent.isStopped = true;
    }

    private void Seek () {
        navMeshAgent.isStopped = false;

        playerTarget = player.transform.position;
        navMeshAgent.SetDestination (playerTarget);

        AlertOthers();

        if ((transform.position - playerTarget).magnitude < proximityDistance) {
            stateMachine.MoveNext (Command.Win);
        }

    }

    private void Alert () {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination (alertTarget);

        if ((transform.position - alertTarget).magnitude < proximityDistance) {
            stateMachine.MoveNext (Command.Patrol);
        }

    }

    private void Patrol () {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination (navMeshTarget);
    }

    private void Die () {
        navMeshAgent.isStopped = true;
    }

    //NAV MESH
    private void UpdateNextNavMeshNode () {
        // Switch to next path node
        int next = nodetogo + step;
        if (next == -1 || next == nodes.Length) {
            // We found the last node.
            if (backAndForth) {
                step = -step;
                next = nodetogo + step;
            } else {
                next = 0;
            }
        }

        nodetogo = next;
    }

    void SetNavMeshTarget () {
        navMeshTarget = nodes[nodetogo].transform.position;
    }
}