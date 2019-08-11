using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    [SerializeField] private List<WayPoint> m_waypoint;


    public WayPoint GetWayPoint(ref int lap, ref int point)
    {
        WayPoint waypoint = null;

        if (point >= m_waypoint.Count)
        {
            lap++;
            point = 0;
        }
        waypoint = m_waypoint[point];
        return waypoint;
    }
}
