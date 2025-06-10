using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName =("SO/Unit Config"))]
public class UnitConfig : ScriptableObject
{
    public Sprite sprite;
    // THem animation neu sprite thay doi va muon co animation
    public int MaxHp;
    public float MoveSpeed;
    public int Damage;
    public float FireRate;
    public float AttackRange;
    public int coinOnDestroy;
    public int coinToUpgrade;
}
