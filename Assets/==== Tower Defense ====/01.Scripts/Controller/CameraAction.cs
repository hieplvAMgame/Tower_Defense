using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAction : MonoBehaviour
{
    [SerializeField] LayerMask layerTile;

    [SerializeField] Camera mainCam;
    public bool isRayToTile;
    private void Awake()
    {
        if (!mainCam)
            mainCam = GetComponent<Camera>();
    }
    Ray ray;
    private void Update()
    {
        ray = mainCam.ScreenPointToRay(Input.mousePosition);
        isRayToTile = Physics.Raycast(ray, 100, layerTile);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(ray.origin, ray.direction*100);
    }
}
