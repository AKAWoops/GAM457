using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//use this to control individual ship behaviour
public class IndividualAISettingsV2 : MonoBehaviour
{
    public float wanderSpeed;
    public float chaseSpeed;
    public float turnSpeed;
    public float attackRange;
    public float AttackSpeed()
    {
        return Random.Range(-1, 2);
    }
    public float frenzyLevel;
    public int detectionRange;
    public LayerMask opponentLayers;
    public LayerMask obstacleLayerMask;
}
