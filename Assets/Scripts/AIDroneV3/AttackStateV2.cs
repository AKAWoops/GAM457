using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateV2 : EnemyBaseStateV2
{
    private float attackReadyTimer;
    private DroneV2 drone;
    private float changeDirectionTime;
    private float direction;//-1.0f = left, 1.0f = right
    private float currentSpeedDir;
    private bool firing = false;

    //constructor
    public AttackStateV2(DroneV2 _drone) : base(_drone.gameObject)
    {
        drone = _drone;
        changeDirectionTime = drone.aISettings.frenzyLevel;
    }

    //this operates like Update() function
    public override Type Tick()
    {
        if (drone.Target == null)
        {
            return typeof(WanderStateV2);
        }
        if (Vector3.Distance(transform.position, drone.Target.position) >= drone.aISettings.attackRange)
        {
            if (firing)
            {
                firing = false;
                drone.FireWeapon(false);
                attackReadyTimer = 10.0f;
            }
            return typeof(ChaseStateV2);
        }
        if (Vector3.Distance(transform.position, drone.Target.position) <= 4.0f)
        {
            if (firing)
            {
                firing = false;
                drone.FireWeapon(false);
                attackReadyTimer = 10.0f;
            }
            return typeof(RetreatStateV2);
        }
        attackReadyTimer -= Time.deltaTime;

        Vector3 dir = transform.position - drone.Target.position;
        Quaternion targetDirection = Quaternion.LookRotation(dir, Vector3.forward);
        targetDirection.z = targetDirection.y;
        targetDirection.x = 0.0f;
        targetDirection.y = 0.0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetDirection, Time.deltaTime * drone.aISettings.turnSpeed);
        changeDirectionTime -= Time.deltaTime;
        if (changeDirectionTime <= 0.0f)
        {
            changeDirectionTime = drone.aISettings.frenzyLevel;
            ChangeDirection();
        }
        transform.Translate(Vector3.right * Time.deltaTime * (drone.aISettings.chaseSpeed * currentSpeedDir));
        if (attackReadyTimer <= 0.0f)
        {
            if (!firing)
            {
                firing = true;
                drone.FireWeapon(true);
                attackReadyTimer = 10.0f;
            }
        }

        return null;
    }

    float ChangeDirection()
    {
        currentSpeedDir = drone.aISettings.AttackSpeed();
        return currentSpeedDir;
    }
}
