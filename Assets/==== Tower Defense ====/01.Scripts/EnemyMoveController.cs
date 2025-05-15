using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMoveController : MonoBehaviour
{
    [SerializeField] PolyNavAgent agent;
    public Waypoint waypoint;
    private void Start()
    {
    }
    public List<Vector2> result;
    [Button("Set Move")]

    public void SetMove(Waypoint wp)
    {
        if (!wp) return;
        waypoint = wp;
        //result = waypoint.ConverPointsToArray();
        //agent.activePath = result;
        //agent.SetDestination(agent.activePath[0]);
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
                     else
                         gameObject.SetActive(false);
                 }
             });
            yield return new WaitForSeconds(.2f);
        }
    }
}
