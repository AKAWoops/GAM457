using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Code
{

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
        ConnectedWayPoint _currentWaypoint;
        ConnectedWayPoint _previousWaypoint;

        bool _travelling;
        bool _waiting;
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
                            ConnectedWayPoint startingWaypoint = allWaypoints[random].GetComponent<ConnectedWayPoint>();

                            if (startingWaypoint != null)
                            {
                                _currentWaypoint = startingWaypoint;
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("failed to find any waypoints for use int he scene.");
                    }
                }
                SetDestination();
            }
        }

        // Update is called once per frame
        public void Update()
        {
            // checking if we are close to the destination.
            if (_travelling && _navMeshAgent.remainingDistance <= 1.0f)
            {
                _travelling = false;
                _waypointsVisited++;

                //if we have to wait then bloody wait
                if (_patrolWaiting)
                {
                    _waiting = true;
                    _waitTimer = 0f;
                }
                else
                {
                    SetDestination();
                }
            }

            //instead of if we are waiting
            if (_waiting)
            {
                _waitTimer += Time.deltaTime;
                if (_waitTimer >= _totalWaitTime)
                {
                    _waiting = false;

                    SetDestination();
                }
            }
        }
        private void SetDestination()
        {
            if (_waypointsVisited > 0)
            {
                ConnectedWayPoint nextWaypoint = _currentWaypoint.NextWaypoint(_previousWaypoint);
                _previousWaypoint = _currentWaypoint;
                _currentWaypoint = nextWaypoint;
            }

            Vector3 targetVector = _currentWaypoint.transform.position;
            _navMeshAgent.SetDestination(targetVector);
            _travelling = true;
        }
    }
}

