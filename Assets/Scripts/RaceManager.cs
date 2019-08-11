using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance;
    [SerializeField] private WayPoints m_waypoints;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public WayPoint GetWayPoint(ref int lap, ref int point)
    {
        WayPoint waypoint = null;
        waypoint = m_waypoints.GetWayPoint(ref lap, ref point);
        return waypoint;
    }
}