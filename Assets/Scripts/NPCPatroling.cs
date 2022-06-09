using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPatroling : MonoBehaviour
{
    //dictates whether the agent waits on ech node.
    [SerializeField]
    bool _patrolWaiting;
    // this is the total time npc waits at each node befor moving on.
    [SerializeField]
    float _totalWaitTime = 3f;

    //this is for switching direction on like a coin toss more like probability of direction change
    [SerializeField]
    float _switchDirectionProbability = 3f;

    //this is a list of all patrol points to visit on the map
    [SerializeField]
    List<Waypoint> _patrolPoints;

    //private variable no serialized fields needed this is for behaviors
    NavMeshAgent _navMeshAgent;
    int _currentPatrolIndex;
    bool _travelling;
    bool _waiting;
    bool _patrolForward;
    float _waitTimer;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (_navMeshAgent == null)
        {
            Debug.LogError("the nav mesh agent component is not attached to " + gameObject.name);
        }
        else
        {
            if (_patrolPoints != null && _patrolPoints.Count >= 2)
            {
                _currentPatrolIndex = 0;
                setDestination();
            }
            else
            {
                Debug.Log("insufficient patrol points for basic patrolling behaviour.");
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        //this will check if it close to the destination point.
        if(_travelling && _navMeshAgent.remainingDistance <= 1.0f)
            {
            _travelling = false;

            //if we are going to wait then bloody wait.
            if (_patrolWaiting)
            {
                _waiting = true;
                _waitTimer = 0f;
            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }
        }

        //instead if we be waiting thew ptrol point will end up changing.
        if (_waiting)
        {
            _waitTimer += Time.deltaTime;
            if(_waitTimer >= _totalWaitTime)
            {
                _waiting = false;

                ChangePatrolPoint();
                SetDestination();
            }
        }
    }

    private void SetDestination()
    {
        if(_patrolPoints != null)
        {
            Vector3 tragetvector = _patrolPoints[_currentPatrolIndex].transform.position;
            _navMeshAgent.SetDestination(targetVector);
            _travelling = true;
        }
    }

    private void ChangePatrolPoint()
    {
        if (UnityEngine.Random.Range(0f, 1f) <= _switchDirectionProbability)
        {
            _patrolForward = !_patrolForward;
        }
        if (_patrolForward)
        {
            // _currentPatrolIndex++;
            //if(_currentPatrolIndex >= _patrolPoints.count)
            //{
            // _currentPatrolIndex = 0;
            //}
            //can also do same as above code 
            //currentpatrol index + 1 then checking if current index exceeeds parreol points if so reset to zero :-)
            _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Count;
        }
        else
        {
            if (--_currentPatrolIndex < 0)
            {
                _currentPatrolIndex = _patrolPoints.Count - 1;
            }
        }
    }
}
