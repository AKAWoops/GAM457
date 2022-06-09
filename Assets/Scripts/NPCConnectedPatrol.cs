using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCConnectedPatrol : MonoBehaviour
{

    //
    [SerializeField]
    bool _patrolWaiting;

    // the total time we wait at each waypoint
    [SerializeField]
    float _totalWaitTime = 3f;

    //the prob os NPC switching Direction
    [SerializeField]
    float _switchProbability = 0.2f;

    //private variables for the base behaviour
    NavMeshAgent _navMeshAgent;
    ConnectedWaypoint _currentWaypoint;
    connectedWaypoint _previousWaypoint;

    bool _travelling;
    bol _waiting;
    float _waitTimer;
    int _waypointsVisited;


    // Start is called before the first frame update
   public void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (_navMeshAgent == null)
        {
            Debug.LogError("The nave mesh agent component is not attached to " + gameObject.name);
        }
        else
        {
            if (_currentWaypoint == null)
            {
                //set it a t random
                //grab all waypoint objects in the current scene.
                GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

                if (allWaypoints.Length > 0)
                {
                    while (_currentWaypoint == null)
                    {
                        int random = UnityEngine.Random.Range(0, allWaypoints.Length);
                        ConnectedWaypoint startingWaypoint = allWaypoints[random].GetComponent<ConnectedWaypoint>();
                    
                        if (startingWaypoint != null)
                        {
                            _currentWaypoint = startingWaypoint;
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
