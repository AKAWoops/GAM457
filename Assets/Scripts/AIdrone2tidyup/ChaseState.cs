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
        transform.Translate(Vector3.forward * Time.deltaTime * GameSettings.DroneSpeed);

        var distance = Vector3.Distance( transform.position,_drone.Target.transform.position);
        if (distance <= GameSettings.AttackRange)
        {
            return typeof(AttackState);
        }
        return null;
        // heads sore not working goes through walls damn it has to be in this State
        // have adjusted layer mask but still going through walls on normal layermask settings I made I have a typo somewhere bah
    }
}
 