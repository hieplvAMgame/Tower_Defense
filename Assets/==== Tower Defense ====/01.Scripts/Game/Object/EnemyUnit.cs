using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : UnitBase
{
    [SerializeField] PolyNavAgent agent;
    [SerializeField] UnitAgent moveController;
    [SerializeField] AnimationController animControl;
    [SerializeField] Waypoint wp;       // tam thoi

    public override Type_Unit TypeUnit => Type_Unit.Zombie;
    public override Type_Unit[] TargetUnitsType => new Type_Unit[] {Type_Unit.Archer_Tower,Type_Unit.Canon_Tower,Type_Unit.Freeze_Tower};

    public override void ApplyConfig(int id)
    {
        base.ApplyConfig(id);
        agent.maxSpeed = _currentConfig.MoveSpeed;
        Debug.Log("Enemy Aplly Config");
    }
    [Button]
    public override void InitUnit(bool isReset = false)
    {
        base.InitUnit(isReset);
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
        animControl?.PlayAnim(AnimName.DYING);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameTag.Home))
        {
            OnDie();
        }
    }
}
