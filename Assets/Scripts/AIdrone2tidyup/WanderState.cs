using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : BaseState
{
    private Vector3? _destination;
    private float stopDistance = 1.0f;
    private float turnSpeed = 1.0f;
    private Quaternion _desiredRotation;
    private Vector3 _direction;
    private readonly LayerMask _layerMask = LayerMask.NameToLayer("walls");
    private float _rayDistance = 3.5f;
    private Quaternion startingAngle = Quaternion.AngleAxis(angle: -60, Vector3.up);
    private Quaternion stepAngle = Quaternion.AngleAxis(angle: 5, Vector3.up);
    private AIDrone2 _drone;

    public WanderState(AIDrone2 drone) : base(drone.gameObject)
    {
        _drone = drone;
    }

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
            Vector3.Distance(a: transform.position, b: _destination.Value) <= stopDistance)
        {
            FindRandomDestination();
        }

        transform.rotation = Quaternion.Slerp(a: transform.rotation, b: _desiredRotation, t: Time.deltaTime * turnSpeed);

        if (IsForwardBlocked())
        {
            transform.rotation = Quaternion.Lerp(a: transform.rotation, b: _desiredRotation, t: 0.2f);
        }
        else
        {
            transform.Translate(translation: Vector3.forward * Time.deltaTime * DroneSettings.DroneSpeed);
        }

        Debug.DrawRay(start: transform.position, dir: _direction * _rayDistance, Color.red);
        while (IsPathBlocked())
        {
            FindRandomDestination();
            Debug.Log(message: "Wall!");
        }

        return null;
    }

    private bool IsForwardBlocked()
    {
        Ray ray = new Ray(origin: transform.position, direction: transform.forward);
        return Physics.SphereCast(ray, radius: 1.0f, _rayDistance, _layerMask);
    }

    private bool IsPathBlocked()
    {
        Ray ray = new Ray(origin: transform.position, _direction);
        return Physics.SphereCast(ray, radius: 0.5f, _rayDistance, _layerMask);
    }

    private void FindRandomDestination()
    {
        Vector3 testPosition = (transform.position + (transform.forward * 4.0f))+ new Vector3(x: UnityEngine.Random.Range(-4.5f, 4.5f), y: 0f, z: UnityEngine.Random.Range(-4.5f, 4.5f));

        _destination = new Vector3(testPosition.x, y: 1.0f, testPosition.z);

        _direction = Vector3.Normalize(_destination.Value - transform.position);
        _direction = new Vector3(_direction.x, y: 0f, _direction.z);
        Debug.Log(message: "Got Direction");
    }
    private Transform CheckForAggro()
    {
        RaycastHit hit;
        var angle = transform.rotation * startingAngle;
        var direction = angle * Vector3.forward;
        var pos = transform.position;
        for (var i = 0; i < 24; i++)
        {
            if (Physics.Raycast(origin: pos, direction, out hit, DroneSettings.AggroRadius))
            {
                var drone = hit.collider.GetComponent<AIDrone2>();
                if (drone != null && drone.Team != gameObject.GetComponent<AIDrone2>().Team)
                {
                    Debug.DrawRay(start: pos, dir: _direction * hit.distance, Color.red);
                    return drone.transform;
                }
                else
                {
                    Debug.DrawRay(start: pos, dir: direction * hit.distance, Color.yellow);
                }
            }
            else
            {
                Debug.DrawRay(start: pos, dir: direction * DroneSettings.AggroRadius, Color.white);
            }
            direction = stepAngle * direction;
        }

        return null;
    }
 }

