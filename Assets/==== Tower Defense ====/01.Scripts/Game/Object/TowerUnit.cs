using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUnit : UnitBase
{
    public override Type_Unit TypeUnit => Type_Unit.Canon_Tower;
    public override Type_Unit[] TargetUnitsType => new Type_Unit[] { Type_Unit.Vampire, Type_Unit.Zombie, Type_Unit.Dragon };

    public override void InitUnit(bool isReset = false)
    {
        base.InitUnit(isReset);
    }
}
