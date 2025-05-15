using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] List<Transform> points = new();
    public List<Transform> Points => points;
    [SerializeField] Color colorLine = Color.blue;
    public bool HaveMore2Point => points.Count >= 2;
    private void OnDrawGizmos()
    {
        if (points.Count < 2)
            return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(points.First().position, .2f);
        for (int i = 1; i < points.Count; i++)
        {
            int index = i;
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(points[index].position, .2f);
            Gizmos.color = colorLine;
            Gizmos.DrawLine(points[index].position, points[index - 1].position);
        }
    }
    public List<Vector2> rs;
    [Button]
    public List<Vector2> ConverPointsToArray()
    {
        rs = points.Select(p => (Vector2)p.position).ToList();
        return rs;
    }
}
