using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Dictionary<Types, BaseState> _availableStates;

    public BaseState CurrentState { get; private set; }
    public event Action<BaseState> OnStateChanged;

    public void SetStates(Dictionary<Types, BaseState> states)
    {
        _availableStates = states;

    }

    private void Update()
    {
        if (CurrentState == null)
        {
            CurrentState = _availableStates.Values.First();
        }

        var nextState = CurrentState?.Tick();

        if (nextState != null &&
            nextState !+ CurrentState?.GetType())
        {
            SwitchToNewState(nextState);
        }
    }

    private void SwitchToNewState(Type nextState)
    {
        //i could inject on transition sound or emitter here would be better than having it on the drone state :-)
        CurrentState = _availableStates[nextState];
        OnStateChanged?.Invoke(CurrentState);
    }

}