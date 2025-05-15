using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public void OnEnter()
    {
        Debug.Log("Idle Enter");
    }

    public void OnExit()
    {
        Debug.Log("Idle Exit");
    }

    public void OnUpdate()
    {
        Debug.Log("Idle Update");
    }
}
