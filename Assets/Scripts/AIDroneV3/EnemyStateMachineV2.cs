using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class EnemyStateMachineV2 : MonoBehaviour
{
    private Dictionary<Type, EnemyBaseStateV2 > availableStates;

    public EnemyBaseStateV2 CurrentState { get; private set; }
    public event Action<EnemyBaseStateV2> OnStateChanged;

    public void SetState(Dictionary<Type, EnemyBaseStateV2> states)
    {
        availableStates = states;
    }

    // Update is called once per frame
    private void Update()
    {
        if (CurrentState == null)
        {
            CurrentState = availableStates.Values.First();
        }

        var nextState = CurrentState?.Tick();

        if (nextState != null && nextState != CurrentState?.GetType())
        {
            SwitchToNewState(nextState);
        }
    }

    private void SwitchToNewState(Type nextState)
    {
        CurrentState = availableStates[nextState];
        OnStateChanged?.Invoke(CurrentState);
    }
}