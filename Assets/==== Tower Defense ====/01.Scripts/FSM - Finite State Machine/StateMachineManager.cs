using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineManager : MonoBehaviour
{
    [ShowInInspector]
    public Dictionary<State, IState> states = new Dictionary<State, IState>();
    [ShowInInspector]
    public IState currentState;

    private void Awake()
    {
        ChangeState(State.Idle);
    }
    [Button]
    public void ChangeState(State state)
    {
        if (states.ContainsKey(state))
        {
            if (currentState != null) 
                currentState.OnExit();
            currentState = states[state];
            currentState.OnEnter();
        }
    }
    private void Update()
    {
        if (currentState != null) 
            currentState.OnUpdate();
    }
}
public enum State
{
    Patrol, Idle
}