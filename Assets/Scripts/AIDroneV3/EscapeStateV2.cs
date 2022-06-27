using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeStateV2 : EnemyBaseStateV2
{
    private DroneV2 drone;
    //constructor
    public EscapeStateV2(DroneV2 _drone) : base(_drone.gameObject)
    {
        drone = _drone;
    }

    //this operates like Update() function
    public override Type Tick()
    {
        transform.Translate(Vector3.down * Time.deltaTime * drone.aISettings.chaseSpeed);
        //active phase shift jump
        return null;
    }
}
