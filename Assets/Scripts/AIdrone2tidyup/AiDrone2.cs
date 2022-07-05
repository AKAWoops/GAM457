using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDrone2 : MonoBehaviour
{
    [SerializeField] private Team _team;
    [SerializeField] private GameObject _laserVisual;
    
    public Transform Target { get; private set; }

    public Team Team => _team;
    
    public StateMachine StateMachine => GetComponent<StateMachine>();


    private void Awake()
    {
        InitializeStateMachine();
    }
    

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()//states dictionary give you intialized states below 
        {   //the reason i have chosen this way is i want to keep a single wander state and re use it saves on recreating new states all the time.
            // removes ineficiencies 
            { typeof(WanderState), new WanderState( this) },
            { typeof(ChaseState), new ChaseState( this) },
            { typeof(AttackState), new AttackState( this) }
          //  { typeof(LowEnergyState), new LowEnergyState( drone: this) }
          //  { typeof(LookForFood), new LookForFood (drone: this) },
          //  { typeof(EatFoodState), new EatFoodState( drone: this) }
        };

        GetComponent<StateMachine>().SetStates(states);
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }

    public void FireWeapon()
    {
        _laserVisual.transform.position = (Target.position + transform.position) / 2f;

        float distance = Vector3.Distance( Target.position, transform.position);
        _laserVisual.transform.localScale = new Vector3( 0.1f,  0.1f, distance);
        _laserVisual.SetActive(value: true);

        StartCoroutine(TurnOffLaser());
    }

    private IEnumerator TurnOffLaser()
    {
        yield return new WaitForSeconds(seconds: 0.25f);
        _laserVisual.SetActive(value: false);

        if (Target != null)
        {
            GameObject.Destroy(Target.gameObject);
        }
    }
}

public enum Team
{
    Red,
    Blue
}
