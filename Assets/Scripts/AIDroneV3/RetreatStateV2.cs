using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatStateV2 : EnemyBaseStateV2
{

    private DroneV2 drone;
    //constructor
    public RetreatStateV2(DroneV2 _drone) : base(_drone.gameObject)
    {
        drone = _drone;
    }

    //this operates like Update() function
    public override Type Tick()
    {
        transform.Translate(Vector3.down * Time.deltaTime * drone.aISettings.chaseSpeed);
        if (Vector3.Distance(transform.position, drone.Target.position) >= drone.aISettings.attackRange)
        {
            drone.FireWeapon(false);
            return typeof(ChaseState);
        }
        return null;
    }
}