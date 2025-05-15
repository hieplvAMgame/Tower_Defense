using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    public void OnEnter()
    {
        Debug.Log("Patrol Enter");
    }

    public void OnExit()
    {
        Debug.Log("Patrol Exit");
    }

    public void OnUpdate()
    {
        Debug.Log("Patrol Update");
    }

}
