using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : UnitBase
{
    [SerializeField] PolyNavAgent agent;
    [SerializeField] EnemyMoveController moveController;
    [SerializeField] AnimationController animControl;
    [SerializeField] Waypoint wp;       // tam thoi


    public override void ApplyConfig(int id)
    {
        base.ApplyConfig(id);
        agent.maxSpeed = currentConfig.MoveSpeed;
        Debug.Log("Enemy Aplly Config");
    }
    [Button]
    public override void InitUnit(Action onHurt = null, Action onHeal = null, Action onDie = null)
    {
        base.InitUnit(onHurt, onHeal, onDie);
        //moveController.SetMove(wp);
    }
    [Button]
    public override void UpLevel(int level = 1)
    {
        base.UpLevel(level);
    }
    [Button]
    public override void ChangeHp(int value)
    {
        base.ChangeHp(value);
    }
    public override void OnDie()
    {
        base.OnDie();
        animControl.PlayAnim(AnimName.DYING);
    }
}
