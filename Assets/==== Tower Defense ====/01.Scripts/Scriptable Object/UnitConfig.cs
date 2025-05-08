using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName =("SO/Unit Config"))]
public class UnitConfig : ScriptableObject
{
    public int MaxHp;
    public float MoveSpeed;
    public int Damage;
    public float FireRate;
    public float AttackRange;
}
