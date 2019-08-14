using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathFinder : MonoBehaviour
{
    public WayPoint m_start;
    public WayPoint m_end;
    List<WayPoint> points;
    // Start is called before the first frame update
    void Update()
    {
        List<WayPoint> been = new List<WayPoint>();
        points = FindShortestNodePath(m_start, m_end, been);
        if (points == null || points.Count == 0)
        {
            Debug.Log("No Path");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (points!=null)
        {
            for (int i = 1; i < points.Count; i++)
            {
                WayPoint prevPoint = points[i - 1];
                WayPoint thisPoint = points[i];
                Gizmos.DrawLine(prevPoint.transform.position, thisPoint.transform.position);
            }
        }
    }

    public List<WayPoint> FindShortestNodePath(WayPoint goal, WayPoint nx, List<WayPoint> visited)
    {
        if (visited.Contains(nx))
        {
            return null;
        }
        else
        {
            visited.Add(nx);
            if (nx == goal)
            {
                List<WayPoint> TmpL = new List<WayPoint>();
                TmpL.Add(nx);
                return TmpL;
            }
            else
            {
                //I use LINQ 
                List<WayPoint> paths = new List<WayPoint>();
                if (nx.nextWayPoint != null)
                {
                    paths.Add(nx.nextWayPoint);
                }
                if (nx.previousWayPoint != null)
                {
                    paths.Add(nx.previousWayPoint);
                }

                var tmp = paths.Where(c => !visited.Contains(c)).OrderBy(t => { return Vector3.Distance(t.transform.position, goal.transform.position);}).ToList();

                if (tmp != null)
                {
                    foreach (WayPoint ww in tmp)
                    {
                        List<WayPoint> TmpR = new List<WayPoint>();
                        TmpR = FindShortestNodePath(goal, ww, visited);
                        if (TmpR != null)
                        {
                            TmpR.Add(nx);
                            return TmpR;
                        }
                    }
                }
                return null;
            }
        }
    }
}
