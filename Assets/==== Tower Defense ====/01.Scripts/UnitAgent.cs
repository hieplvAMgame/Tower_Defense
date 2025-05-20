using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitAgent : MonoBehaviour
{
    [SerializeField] PolyNavAgent agent;
    [SerializeField] UnitBase _unit;

    public UnitBase Unit =>_unit;

    private Waypoint waypoints;
    public float waypointReachThreshold = 0.2f;
    private int currentIndex = 0;
    void Start()
    {
        agent.stoppingDistance = 0;
        agent.slowingDistance = 0;
        agent.decelerationRate = 0;
    }
    public void Setup(Waypoint wp, PolyNav2D map)
    {
        waypoints = wp;
        agent.map = map;
    }
    [Button]
    public void StartMove()
    {
        if (waypoints.Points.Count > 0)
        {
            MoveToNext();
        }
    }
    void Update()
    {
        if (!agent.hasPath || agent.pathPending)
            return;

        float dist = Vector2.Distance(agent.position, waypoints.Points[currentIndex].position);
        if (dist <= waypointReachThreshold)
        {
            currentIndex++;
            if (currentIndex < waypoints.Points.Count)
                MoveToNext();
            else
                agent.Stop();
        }
    }
    void MoveToNext()
    {
        agent.SetDestination(waypoints.Points[currentIndex].position);
    }
}
