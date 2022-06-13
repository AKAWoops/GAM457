using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : BaseState
{
    private Vector3? _destination;
    private float stopDistance = 1f;
    private float turnSpeed = 1f;
    private readonly LayerMask _layerMask = LayerMask.NameToLayer("Walls");
    private float _raydistance = 3.5f;
    private Quaternion _desiredRotation;
    private Vector3 _direction;
    private DroneState _drone;

    public WanderState(Drone drone) : base(drone.gameObject)
    {
        _drone = drone;
    }

    public override Type tick()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
