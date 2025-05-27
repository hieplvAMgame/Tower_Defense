using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig
{

}

public enum Type_Unit
{
    Archer = 0,
    Berserker = 1,
    Wizard = 2,

    Archer_Tower = 20,
    Canon_Tower = 21,
    Freeze_Tower = 22,


    Zombie = 100,
    Dragon = 101,
    Vampire = 102
}
public struct AnimName
{
    public const string WALKING = "Walking";
    public const string DYING = "Dying";
}
public struct GameTag
{
    public const string Home = "Home";
    public const string Unit = "Unit";
}

public static class Extentions
{
    public static bool CanAttack(this UnitBase self, UnitBase target)
    {
        foreach(var x in self.TargetUnitsType)
        {
            if (target.TypeUnit == x)
                return true;
        }
        return false;
    }
}