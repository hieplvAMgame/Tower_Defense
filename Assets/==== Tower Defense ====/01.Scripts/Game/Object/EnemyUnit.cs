using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : UnitBase
{
    [SerializeField] PolyNavAgent agent;
    [SerializeField] EnemyMoveController moveController;
    [SerializeField] Waypoint wp;       // tam thoi
    [Button("Init")]
    public override void InitUnit()
    {
        CurrentLevel = 0;
        ApplyConfig(CurrentLevel);
    }
    public override void ApplyConfig(int id)
    {
        currentConfig = Config[id];
        CurrentHP = currentConfig.MaxHp;
        agent.maxSpeed = currentConfig.MoveSpeed;
    }
    [Button]
    public void SpawnUnit()
    {
        moveController.SetMove(wp);
    }
    [Button]
    public void UpLevel()
    {
        CurrentLevel++;
        ApplyConfig(CurrentLevel);
    }
}
