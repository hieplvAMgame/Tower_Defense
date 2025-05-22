using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Game Management Unit")]
public class GameManagement : ScriptableObject
{
    [Header("PROJECTILES LIST")]
    public List<GameObject> projectile = new();

    [Header("TOWERS LIST")]
    public List<GameObject> towers = new();

}
