using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathFinder : MonoBehaviour
{
    public static List<WayPoint> FindShortestNodePath(WayPoint goal, WayPoint nx, List<WayPoint> visited)
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
