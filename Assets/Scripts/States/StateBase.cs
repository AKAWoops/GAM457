using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateBase : MonoBehaviour
{
    public enum STATE
    {
        IDLE, PATROL, PURSUE, ATTACK, SLEEP, GATHERFOOD, EAT

    };
    public enum EVENT
    {
        ENTER, UPDATE, EXIT

    };

    public STATE name;
    protected EVENT stage;
    protected GameObject npc;
    protected Animator anim;
    protected Transform player;
    protected STATE nextState;
    protected NavMeshAgent agent;

    float visDistance = 10.0f;
    float visAngle = 30.0f;
    float ShootDist = 7.0f;

    public StateBase(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
    {
        npc = _npc;
        agent = _agent;
        anim = _anim;
        stage = EVENT.ENTER;
        player = _player;
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
