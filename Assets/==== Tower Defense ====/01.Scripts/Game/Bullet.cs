using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] GameObject onHitVfx;
    Transform target = null;
    private Vector2 _dir;
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        if (!target) return;
        _dir = target.position - transform.position;
        transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg - 90);
    }
    private void FixedUpdate()
    {
        if (!target) return;
        rb.velocity = _dir.normalized * speed;
    }
    public void Reset()
    {
        target = null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameTag.Unit))
        {
            Reset();
            Vector2 contactPoint = collision.bounds.center ;
            GameObject hitVfx = ObjectPooling.Instance.GetObjFromPool(onHitVfx);
            hitVfx.transform.position = contactPoint;
            hitVfx.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
