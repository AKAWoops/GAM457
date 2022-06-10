using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;


namespace Assets.Code
{
    public class ConnectedWayPoint : Waypoint
    {
        [SerializeField]
        protected float _connectivityRadius = 50f;

        List<ConnectedWayPoint> _connections;

        // Start is called before the first frame update
        public void Start()
        {
            //Grab all waypoint objects positions in scene.
            GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

            //Create a list of the waypoionts detected you can refer to later.
            _connections = new List<ConnectedWayPoint>();

            //Now check if they are a connected waypoint.
            for (int i = 0; i < allWaypoints.Length; i++)
            {
                ConnectedWayPoint nextWaypoint = allWaypoints[i].GetComponent<ConnectedWayPoint>();

                // we have found the waypoint
                if (nextWaypoint != null)
                {
                    if (Vector3.Distance(this.transform.position, nextWaypoint.transform.position) <= _connectivityRadius && nextWaypoint != this)
                    {
                        _connections.Add(nextWaypoint);
                    }
                }
            }
        }

        public override void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, debugDrawRadius);

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, _connectivityRadius);
        }
        public ConnectedWayPoint NextWaypoint(ConnectedWayPoint previousWaypoint)
        {
            if (_connections.Count == 0)
            {
                //no waypoints??? return a null and complain it cant fin enough points
                Debug.LogError("not enough way points in list.");
                return null;
            }
            else if (_connections.Count == 1 && _connections.Contains(previousWaypoint))
            {
                // Only one way point and it's previous one ? just use that ahaha
                return previousWaypoint;
            }
            else // otherwise go and find a random points that is not the one we just did being previous
            {
                ConnectedWayPoint nextWaypoint;
                int nextIndex = 0;

                do
                {
                    nextIndex = UnityEngine.Random.Range(0, _connections.Count);
                    nextWaypoint = _connections[nextIndex];

                }
                while (nextWaypoint == previousWaypoint);

                return nextWaypoint;
            }
        }
    }
}