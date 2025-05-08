using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestMoveFollowWaypoint : MonoBehaviour
{
    [SerializeField] PolyNavAgent agent;
    [SerializeField] Waypoint waypoint;
    private void Start()
    {
       
        //transform.position = waypoint.Points.First().position;
        //currentIndex = 1;
        //currentTarget = waypoint.Points[currentIndex];
        //agent.SetDestination(currentTarget.position);
    }
    public void SetMove(Waypoint wp)
    {
        if(!wp) return;
        waypoint = wp;
        StartCoroutine(MoveThroughWP());
    }
    Transform currentTarget;
    int currentIndex = 0;
    bool rs;
    IEnumerator MoveThroughWP()
    {
        if (!waypoint.HaveMore2Point) yield break;
        transform.position = waypoint.Points.First().position;
        currentIndex = 1;
        currentTarget = waypoint.Points[currentIndex];
        while (currentIndex <= waypoint.Points.Count - 1)
        {
            agent.SetDestination(currentTarget.position, reached =>
             {
                 if (reached)
                 {
                     currentIndex++;
                     if (currentIndex <= waypoint.Points.Count - 1)
                         currentTarget = waypoint.Points[currentIndex];
                 }
             });
            yield return new WaitForSeconds(.2f);
        }
    }
}
