using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    [SerializeField] CircleCollider2D circleRange;
    [SerializeField] UnitConfig currentConfig;
    [ShowInInspector]
    public Queue<GameObject> queue = new();
    public GameObject currentTarget = null;
    private void Awake()
    {
        Setup(currentConfig);
    }
    [Button]
    public void Setup(UnitConfig config)
    {
        circleRange.radius = config.AttackRange;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        if (currentConfig)
            Gizmos.DrawWireSphere(transform.position, currentConfig.AttackRange);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!queue.Contains(collision.gameObject))
        {
            queue.Enqueue(collision.gameObject);
            Debug.Log($"Add {gameObject.name}");
            if (!currentTarget)
                currentTarget = queue.Dequeue();
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        ChangeTarget();
    }
    public void ChangeTarget(GameObject checker = null)
    {
        if (currentTarget || currentTarget.GetInstanceID() == checker.GetInstanceID())
        {
            if (queue.Count > 0)
                currentTarget = queue.Dequeue();
            else
                currentTarget = null;
        }
    }
}
