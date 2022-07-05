using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : BaseState
{
    private Vector3? _destination; //nullable vector 3 
    private float stopDistance = 1f;
    private float turnSpeed = 1f;
    //private readonly LayerMask _layerMask = LayerMask.NameToLayer("Walls");
    //private readonly int _layerMask = 1 << 8;
    LayerMask _layerMask = 1 << 8; // found this fixes the layer mask issue
    private float _rayDistance = 3.5f;
    private Quaternion _desiredRotation;
    private Vector3 _direction;
    private AiDrone2 _drone;
    
    public WanderState(AiDrone2 drone) : base(drone.gameObject)
    {
        _drone = drone;
    }
    // ReSharper disable Unity.PerformanceAnalysis
    public override Type Tick()
    {
        //check for target in range
        var chaseTarget = CheckForAggro();
        if (chaseTarget != null)
        {
            _drone.SetTarget(chaseTarget);
            return typeof(ChaseState);
        }
        //if there is no targets just wander and wander and wander
        if (_destination.HasValue == false ||
            Vector3.Distance( transform.position, _destination.Value) <= stopDistance)
        {
            FindRandomDestination();
        }
        transform.rotation = Quaternion.Slerp( transform.rotation, _desiredRotation, Time.deltaTime * turnSpeed);

        if (IsForwardBlocked())
        {
            transform.rotation = Quaternion.Lerp( transform.rotation, _desiredRotation,  0.2f);
        }
        else
        {
            transform.Translate( Vector3.forward * Time.deltaTime * GameSettings.DroneSpeed);
            //rigid body call add relative force :-) on z Axis
            //Vector3.forward * Time.deltaTime * GameSettings.DroneSpeed
        }

        Debug.DrawRay( transform.position, _direction * _rayDistance, Color.red);
        while (IsPathBlocked())
        {
            FindRandomDestination();
            Debug.Log(message: "Wall!");
        }

        return null;
    }

    private bool IsForwardBlocked()
    {
        Ray ray = new Ray( transform.position, transform.forward);
        return Physics.SphereCast(ray, 0.5f, _rayDistance, _layerMask);
        //RaycastHit hitinfo;
        //bool isForwardBlocked = Physics.SphereCast(ray, radius: 0.5f, out hitinfo, _rayDistance, _layerMask);
        //return isForwardBlocked;

    }

    private bool IsPathBlocked()
    {
        Ray ray = new Ray(transform.position, _direction);
        return Physics.SphereCast(ray, 0.5f, _rayDistance,_layerMask);
    }

    private void FindRandomDestination()
    {
        Vector3 testPosition = (transform.position + (transform.forward * 4f))
            + new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), 0f, UnityEngine.Random.Range(-4.5f, 4.5f));

        _destination = new Vector3(testPosition.x, 1f, testPosition.z);

        _direction = Vector3.Normalize(_destination.Value - transform.position);
        _direction = new Vector3(_direction.x, 0f, _direction.z);
        _desiredRotation = Quaternion.LookRotation(_direction);//lmao i never called this quanternion statement never made it sucha spanner
        Debug.Log(message: "Got Direction");
    }

    private Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    private Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);
    private Transform CheckForAggro()
    {
        RaycastHit hit;
        var angle = transform.rotation * startingAngle;
        var direction = angle * Vector3.forward;
        var pos = transform.position;
        for (var i = 0; i < 24; i++)
        {
            if (Physics.Raycast(pos, direction, out hit, GameSettings.AggroRadius))
            {
                var drone = hit.collider.GetComponent<AiDrone2>();
                if (drone != null && drone.Team != gameObject.GetComponent<AiDrone2>().Team)
                {
                    Debug.DrawRay( pos, _direction * hit.distance, Color.red);
                    return drone.transform;
                }
                else
                {
                    Debug.DrawRay( pos,  direction * hit.distance, Color.yellow);
                }
            }
            else
            {
                Debug.DrawRay( pos, direction * GameSettings.AggroRadius, Color.white);
            }
            direction = stepAngle * direction;
            
            // add this line in to fix my ai stupid idiot i am missed th line lmao _desiredRotation

        }

        return null;
    }
 }

