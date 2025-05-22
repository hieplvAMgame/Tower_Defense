using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    [SerializeField] bool showGizmos;
    [SerializeField] CircleCollider2D circleRange;
    [SerializeField] UnitConfig currentConfig;
    [ShowInInspector]
    public List<UnitBase> queue = new();
    public UnitBase currentTarget = null;
    [Space]
    [SerializeField] GameObject bullet;
    private void Awake()
    {
        Setup(currentConfig);
        bullet = Resources.Load<GameManagement>("GameConfig").projectile.FirstOrDefault();
    }
    [Button]
    public void Setup(UnitConfig config)
    {
        circleRange.radius = config.AttackRange;
    }

#region AIM TARGET

    UnitBase _unit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(GameTag.Unit)) return;
        if (collision.TryGetComponent(out _unit))
        {
            if (!queue.Contains(_unit))
            {
                queue.Add(_unit);
                Debug.Log($"Add {gameObject.name}");
                if (!currentTarget)
                    currentTarget = queue.FirstOrDefault();
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(GameTag.Unit)) return;
        if (collision.TryGetComponent(out _unit))
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
        go.transform.position = transform.position;
        go.GetComponent<Bullet>().SetTarget(currentTarget.transform);
        go.gameObject.SetActive(true);
    }
    #endregion
    private void OnDrawGizmos()
    {
        if (!showGizmos) return;
        Gizmos.color = Color.magenta;
        if (currentConfig)
        {
            Gizmos.DrawWireSphere(transform.position, currentConfig.AttackRange);
            if (currentTarget)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(transform.position, currentTarget.transform.position);
            }
        }
    }
}
