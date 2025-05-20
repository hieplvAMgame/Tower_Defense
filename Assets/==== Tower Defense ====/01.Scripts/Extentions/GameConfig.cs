using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig
{

}

public enum Type_Unit
{
    Archer,
    Berserker,
    Wizard
}
public struct AnimName
{
    public const string WALKING = "Walking";
    public const string DYING = "Dying";
}
public struct GameTag
{
    public const string Home = "Home";
}