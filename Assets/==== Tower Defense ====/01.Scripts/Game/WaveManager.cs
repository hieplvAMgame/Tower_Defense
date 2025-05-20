using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] Wave wave;
    [SerializeField] Transform tfSpawn;
    [SerializeField] List<AttackSystem> atkSystem = new();
    int _curQuantity;
    private int CurrentQuantity
    {
        get => _curQuantity;
        set
        {
            if (value <= 0)
            {
                _curQuantity = 0;
                onCurrentWave?.Invoke(0);
            }
            else
                _curQuantity = value;
        }
    }
    Action<int> onCurrentWave = null;
    // Refactor sau
    [SerializeField] Waypoint waypoint;
    [SerializeField] PolyNav2D navMap;
    GameObject go;
    [Button]
    public void SpawnWave()
    {
        onCurrentWave = lastWave => Debug.Log("Chau cuoi cung die!");
        StartCoroutine(CoSpawnWave(tfSpawn.position, _ =>
        {
            CurrentQuantity--;
        }));
        _curQuantity = wave.quantity;
    }
    IEnumerator CoSpawnWave(Vector3 posSpawn, Action<GameObject> onUnitDie = null)
    {
        int count = 0;
        while (count <= wave.quantity - 1)
        {
            count++;
            SpawnObj(posSpawn, onUnitDie).name = $"Enemy {count}";
            yield return new WaitForSeconds(wave.intervalTime);
        }
    }
    private GameObject SpawnObj(Vector3 posSpawn, Action<GameObject> onUnitDie = null)
    {
        go = ObjectPooling.Instance.GetObjFromPool(wave.unitPrefabs);
        go.transform.position = posSpawn;
        go.SetActive(true);
        if (go.TryGetComponent(out UnitAgent enemy))
        {
            enemy.Unit.InitUnit(onDie: obj =>
            {
                foreach (var item in atkSystem)
                {
                    item.ChangeTarget(obj);
                }
                onUnitDie(obj);
            });
            enemy.Setup(waypoint, navMap);
            enemy.StartMove();
        }
        
        return go;
    }
}
[System.Serializable]
public class Wave
{
    public GameObject unitPrefabs;
    public float intervalTime;
    public int quantity;
}