using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ennemy : MonoBehaviour {

    public Player player;
    public float deltaDistance = 2f;


    //Nav Mesh parameters
    public bool clockwise;
    public GameObject PathFinding;
	private NavMeshAgent navMeshAgent;
    private PathFindingNode[] nodes;
    public int startnode;
    public int nodetogo;

    //Targets
    private Vector3 navMeshTarget = Vector3.zero;
    private Vector3 alertTarget = Vector3.zero;
    private Vector3 playerTarget = Vector3.zero;

    

    private EnnemyStateMachine stateMachine;

	// Use this for initialization
	void Start () {
        stateMachine = new EnnemyStateMachine();
        //GameObject goPlayer = GameObject.FindGameObjectWithTag("Player");
        //if (goPlayer != null)
        //    goPlayer.GetComponents<Player>();

        //Init Nav Mesh Agent
        nodetogo = startnode;
        nodes = PathFinding.GetComponentsInChildren<PathFindingNode>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        print("Number of Nodes: " + nodes.Length);
	}
	
	// Update is called once per frame
	void Update () {
        UpdateBehaviorFromStateMachine();
	}

    private void UpdateBehaviorFromStateMachine()
    {
        switch (stateMachine.currentState)
        {
            case State.Win:
                Idle();
                break;
            case State.Patrolling:
                Patrol();
                break;
            case State.Alerted:
                Alert();
                break;
            case State.Seeking:
                Seek();
                break;
            case State.Dead:
                Die();
                break;
        }
    }

    //EVENTS
    //OUT
    private void AlertOthers()
    {
        //TODO
        //Get Near enemies
        //For each near ennemis - alert
    }

    //IN
    public void Alert (Vector3 position)
    {
        alertTarget = position;
        stateMachine.MoveNext(Command.Alert);
        Debug.Log("ALERT");
    }

    public void Kill()
    {
        stateMachine.MoveNext(Command.Die);
        Debug.Log("DEAD");
    }

    public void PlayerEnterFieldOfVision()
    {
        stateMachine.MoveNext(Command.Seek);
        Debug.Log("SEEK");
    }

    public void PlayerExitFieldOfVision()
    {
        alertTarget = playerTarget;
        stateMachine.MoveNext(Command.LoseTrack);
        Debug.Log("ALERT");
    }

    //IN - TRIGGER EVENT
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Node") && other.GetComponent<PathFindingNode>().nodeNumber == nodetogo)
        {
            UpdateNavMeshPath();
        }
    }


    //BEHAVIORS
    private void Idle()
    {
        navMeshAgent.isStopped = true;
    }

    private void Seek()
    {
        navMeshAgent.isStopped = false;

        playerTarget = player.transform.position;
        navMeshAgent.SetDestination(playerTarget);

        if ((transform.position - playerTarget).magnitude < deltaDistance)
        {
            Debug.Log("WIN !!!! (ENNEMY)");
            stateMachine.MoveNext(Command.Win);
        }
            
    }

    private void Alert()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(alertTarget);

        if ((transform.position - alertTarget).magnitude < deltaDistance)
        {
            Debug.Log("PATROLLING");
            stateMachine.MoveNext(Command.Patrol);
        }
            
    }

    private void Patrol()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(navMeshTarget);
    }

    private void Die()
    {
        navMeshAgent.isStopped = true;
    }


    //NAV MESH
    private void UpdateNavMeshPath()
    {
        //Calculate index
        int increment = clockwise ? 1 : -1;
        nodetogo += increment;

        if (nodetogo < 0)
            nodetogo += nodes.Length;
        else
            nodetogo = nodetogo % nodes.Length;

        //Update navmesh target
        navMeshTarget = nodes[nodetogo].transform.position;
    }
}
