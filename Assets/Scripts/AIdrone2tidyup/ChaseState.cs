using System;
using UnityEngine;

public class ChaseState : BaseState
{
    private AiDrone2 _drone;

    public ChaseState(AiDrone2 drone) : base(drone.gameObject)
    {
        _drone = drone;
    }

    public override Type Tick()
    {
        if (_drone.Target == null)
            return typeof(WanderState);

        transform.LookAt(_drone.Target);
        transform.Translate(translation: Vector3.forward * Time.deltaTime * DroneSettings.DroneSpeed);

        var distance = Vector3.Distance(a: transform.position, b: _drone.Target.transform.position);
        if (distance <= DroneSettings.AttackRange)
        {
            return typeof(AttackState);
        }
        return null;
        // heads sore not working goes through walls damn it has to be in this State
    }
}
 