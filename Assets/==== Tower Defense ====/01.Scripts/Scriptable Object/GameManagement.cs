using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Game Management Unit")]
public class GameManagement : ScriptableObject
{
    [Header("UNIT BULLET CONFIG")]
    public List<UnitBulletConfig> projectile = new();

    public GameObject GetBullet(Type_Unit type) => projectile.FirstOrDefault(bullet => bullet.typeUnit == type).bulletPrefab;
}
[System.Serializable]
public class UnitBulletConfig
{
    public Type_Unit typeUnit;
    public GameObject bulletPrefab;
}
