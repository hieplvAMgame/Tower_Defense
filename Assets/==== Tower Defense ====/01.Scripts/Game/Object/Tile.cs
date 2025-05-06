using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Collider col3d;
    [SerializeField] SpriteRenderer spriteRenderer;
    [Space, Header("===== CONFIG=====")]
    public float time = 2f;
    public bool isTap = false;
    public bool IsBuilable = false;
    Coroutine coro;
    [Button]
    public void Setup(bool buildAble)
    {
        col3d.enabled = buildAble;
        spriteRenderer.color = buildAble ? Color.green : Color.red;
        IsBuilable = buildAble;
    }
    [ShowInInspector]
    public int X { get; private set; }
    [ShowInInspector]
    public int Y { get; private set; }
    public void SetPos(int x, int y)
    {
        X = x; Y = y;
    }
    private void OnMouseDown()
    {
        if (coro != null) StopCoroutine(coro);
        coro = StartCoroutine(IEHoldDownTile());
    }
    private void OnMouseUp()
    {
        if (coro != null) StopCoroutine(coro);
        isTap = false;
    }
    IEnumerator IEHoldDownTile()
    {
        yield return new WaitForSeconds(time);
        isTap = true;
        Debug.Log($"Select Tile {gameObject.name}");
    }
}
