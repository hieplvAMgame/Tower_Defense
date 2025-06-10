using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveEntry : MonoBehaviour
{
    // Tam thoi
    public int id;
    [SerializeField] TowerUnit tower;
    [SerializeField] Wave wave;
    [SerializeField] Transform tfSpawn;
    [SerializeField] List<AttackSystem> atkSystem = new();

    int _curQuantity;
    bool isClear => CurrentQuantity == 0;
    private int CurrentQuantity
    {
        get => _curQuantity;
        set
        {
            if (value <= 0)
            {
                _curQuantity = 0;
            }
            else
                _curQuantity = value;
            onChangeQuantityUnit?.Invoke(_curQuantity);
        }
    }
    Action<int> onChangeQuantityUnit = null;
    // Refactor sau
    [SerializeField] Waypoint waypoint;
    [SerializeField] PolyNav2D navMap;
    GameObject go;
    public List<UnitBase> units = new();

    public void Setup(Wave wave)
    {
        if(wave.idWaveEntry!= id)
        {
            Debug.LogError("Wave not match entry id. Pls check config!");
            return;
        }
        this.wave = wave;
    }

    [Button]
    public void SpawnWave()
    {
        units.Clear();
        onChangeQuantityUnit = num =>
        {
            if (num <= 0)
            {
                Debug.Log($"Current = {num}");
                // Handle event khi clear wave o entry nay
            }
        };
        StartCoroutine(CoSpawnWave(tfSpawn.position, _ =>
        {
            CurrentQuantity--;
        }));
        _curQuantity = wave.TotalQuantity;
    }
    IEnumerator CoSpawnWave(Vector3 posSpawn, Action<GameObject> onUnitDie = null)
    {
        int count = 0;
        int index = 0;
        while (index < wave.crowds.Count)
        {
            count++;
            SpawnObj(index,posSpawn, onUnitDie).name = $"Enemy {count}";
            if (count >= wave.crowds[index].quantity)
            {
                count = 0;
                index++;
            }
            yield return new WaitForSeconds(wave.intervalTime);
        }
    }
    // TODO: Refactor, add event on unit die to tower
    private GameObject SpawnObj(int indexUnit, Vector3 posSpawn, Action<GameObject> onUnitDie = null)
    {
        go = ObjectPooling.Instance.GetObjFromPool(wave.crowds[indexUnit].unitPrefabs);
        // Them listener de nghe su kien chet
        go.transform.position = posSpawn;
        go.SetActive(true);
        units.Add(go.GetComponent<UnitBase>());
        if (go.TryGetComponent(out UnitAgent enemy))
        {

            // TODO: Refactor
            tower.AddEvent(onDie: enemy.Unit.AttackSystem.OnRemoveTargetInQueue);
            enemy.Unit
                .AddEvent(onDie: _ =>
                {
                    tower.AttackSystem.OnRemoveTargetInQueue(_);
                    onUnitDie?.Invoke(_.gameObject);
                })
                .InitUnit();
            enemy.Setup(waypoint, navMap);
            enemy.StartMove();
        }
        return go;
    }
}
// Tower: 5
// Tower[]