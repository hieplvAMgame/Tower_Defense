using Sirenix.Utilities.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase: MonoBehaviour
{
    protected int CurrentHP;
    protected int CurrentLevel;
    public UnitConfig[] Config;
    public virtual Type_Unit TypeUnit { get; }
    public bool IsAlive { get; protected set; }

    protected UnitConfig currentConfig;

    public abstract void InitUnit();
    public abstract void ApplyConfig(int id);
}
