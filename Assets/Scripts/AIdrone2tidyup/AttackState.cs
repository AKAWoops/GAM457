using System;
using UnityEngine;

public class AttackState : BaseState
{
    private float _attackReadyTimer;
    private AIDrone2 _drone;

    public AttackState(AIDrone2 drone) : base(drone.gameObject)
    {
        _drone = drone;
    }

    public override Type Tick()
    {
        if (_drone.Target == null)
            return typeof(WanderState);
// could posibly add if gets out of attack range go back to chase state
// if my heallth is low run away 
        _attackReadyTimer -= Time.deltaTime;

        if (_attackReadyTimer <= 0f)
        {
            Debug.Log(message: "Attack!");
            _drone.FireWeapon();
        }
        return null;
    }
}
