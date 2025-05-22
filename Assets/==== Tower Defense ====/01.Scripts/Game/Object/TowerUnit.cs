using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUnit : UnitBase
{
    [SerializeField] AttackSystem atkSystem;

    public AttackSystem AttackSystem => atkSystem;

    public override void InitUnit(Action onHurt = null, Action onHeal = null, Action<UnitBase> onDie = null)
    {
        base.InitUnit(onHurt, onHeal, onDie);
    }
}
