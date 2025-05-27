using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class AttackSystem : MonoBehaviour
{
    [SerializeField] bool showGizmos;
    [SerializeField] CircleCollider2D circleRange;

    [ShowInInspector]
    public List<UnitBase> queue = new();
    UnitBase currentTarget = null;
    GameObject bullet;
    UnitBase _owner;

    float _countTime = 0;
    public void Setup(UnitBase owner)
    {
        _owner = owner;
        circleRange.radius = owner.CurrentConfig.AttackRange;
        bullet = Resources.Load<GameManagement>("GameConfig").GetBullet(_owner.TypeUnit);
        _countTime = _owner.CurrentConfig.FireRate;
    }
    private void Update()
    {
        _countTime += Time.deltaTime;
        if (!currentTarget) return;
        if (_countTime >= _owner.CurrentConfig.FireRate)
        {
            Attack();
            _countTime = 0;
            return;
        }
    }
    #region AIM TARGET

    UnitBase _unit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(GameTag.Unit)) return;
        if (!collision.TryGetComponent(out _unit)) return;
        Debug.Log($"Is Tower = {_unit is TowerUnit}");
        if (!_owner.CanAttack(_unit))
        {
            _unit = null;
            return;
        }
        if (!queue.Contains(_unit))
        {
            queue.Add(_unit);
            Debug.Log($"{_owner.gameObject.name}Add {_unit.gameObject.name} to target");
            if (!currentTarget)
                currentTarget = queue.FirstOrDefault();
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(GameTag.Unit)) return;
        if (collision.TryGetComponent(out _unit) && _owner.CanAttack(_unit))
        {
            if (_unit.IsAlive)
            {
                ChangeTarget();
            }
        }
    }
    public void CheckRemove(UnitBase checker = null)
    {
        if (queue.Contains(checker))
        {
            if (currentTarget == checker)
            {
                ChangeTarget();
            }
            else
            {
                queue.Remove(checker);
            }
        }
    }
    // TODO: Handle logic after an unit die
    public void ChangeTarget()
    {
        if (currentTarget)
        {
            queue.Remove(currentTarget);
            if (queue.Count > 0)
                currentTarget = queue.FirstOrDefault();
            else
                currentTarget = null;
        }
    }
    public void OnRemoveTargetInQueue(UnitBase target)
    {
        // Logic check: enemy co trong target queue k
        // neu co thi remove
        Debug.Log($"Check remove {target.gameObject.name}");
        CheckRemove(target);
    }
    #endregion
    GameObject go;
    #region SHOOT 
    [Button]
    public void Attack()
    {
        go = ObjectPooling.Instance.GetObjFromPool(bullet);
        go.transform.position = _owner.transform.position;
        go.GetComponent<Bullet>().Setup(currentTarget.transform, _owner);
        go.gameObject.SetActive(true);
    }
    #endregion
    private void OnDrawGizmos()
    {
        if (!showGizmos) return;
        Gizmos.color = Color.magenta;
        if (_owner && _owner.CurrentConfig)
        {
            Gizmos.DrawWireSphere(_owner.transform.position, _owner.CurrentConfig.AttackRange);
            if (currentTarget)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(_owner.transform.position, currentTarget.transform.position);
            }
        }
    }
}
// MVC: Model - View - Control