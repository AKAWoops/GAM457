using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DroneV2 : MonoBehaviour
{
    public IndividualAISettingsV2 aISettings;
    [SerializeField] 
    private GameObject _laserVisual;

    public Transform Target { get; private set; }

    public EnemyStateMachineV2 EnemyStateMachine => GetComponent<EnemyStateMachineV2>();

    private void Awake()
    {
        InitialiseStateMachine();
    
    }

    private void InitialiseStateMachine()
    {
        //look at adding states like RetreatState, FrenzyState
        var states = new Dictionary<Type, EnemyBaseStateV2>()
        {
            { typeof(WanderStateV2), new WanderStateV2(this)},
            { typeof(ChaseStateV2), new ChaseStateV2(this)},
            { typeof(AttackStateV2), new AttackStateV2(this)},
            { typeof(EscapeStateV2), new EscapeStateV2(this)},
            { typeof(RetreatStateV2), new RetreatStateV2(this)}
        };
        GetComponent<EnemyStateMachineV2>().SetState(states);
    }

    public void SetTarget(Transform _target)
    {
        Target = _target;
    }

    public void FireWeapon(bool _fireState)
    {
        _laserVisual.transform.position = (Target.position + transform.position) / 2f;

        float distance = Vector3.Distance(a: Target.position, b: transform.position);
        _laserVisual.transform.localScale = new Vector3(x: 0.1f, y: 0.1f, z: distance);
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
