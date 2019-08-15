using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class SatNav : MonoBehaviour
{
    private enum SatNavType
    {
        DRIVE_AROUND,
        SAT_NAV

    }

    [SerializeField] private SatNavType m_type;
    CarAIControl m_car;

    public WayPoint m_start;
    public WayPoint m_end;
    private WayPoint m_current;
    private List<WayPoint> m_points;

    private void Start()
    {
        m_car = GetComponent<CarAIControl>();
        m_current = m_start;
        m_points = new List<WayPoint>();
        if (m_type == SatNavType.DRIVE_AROUND)
        {
            m_car.SetTarget(m_start);
        }
        else
        {
            List<WayPoint> route = new List<WayPoint>();
            m_points = PathFinder.FindShortestNodePath(m_current, m_end, route);
            if (m_points.Count > 0)
            {
                m_car.SetTarget(m_points[0]);
            }
        }
    }

    private void Update()
    {
        if (m_car.ReachedTarget())
        {
            if (m_type == SatNavType.DRIVE_AROUND)
            {
                if (m_car.GetCurrentWaypoint().branches != null && m_car.GetCurrentWaypoint().branches.Count > 0)
                {
                    bool branch = Random.Range(0f, 1f) <= m_car.GetCurrentWaypoint().branchRatio;
                    if (branch)
                    {
                        m_current = m_car.GetCurrentWaypoint().branches[Random.Range(0, m_car.GetCurrentWaypoint().branches.Count)];
                        m_car.SetTarget(m_current);
                    }
                    else
                    {
                        m_current = m_car.GetCurrentWaypoint().nextWayPoint;
                        m_car.SetTarget(m_current);
                    }
                }
                else
                {
                    m_current = m_car.GetCurrentWaypoint().nextWayPoint;
                    m_car.SetTarget(m_current);
                }
            }
            else
            {
                if (m_current == m_end)
                {
                    m_car.Stop();
                }
                else
                {
                    List<WayPoint> route = new List<WayPoint>();
                    m_points = PathFinder.FindShortestNodePath(m_current, m_end, route);
                    if (m_points.Count > 1)
                    {
                        m_current = m_points[1];
                        m_car.SetTarget(m_current);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (m_points != null)
        {
            for (int i = 1; i < m_points.Count; i++)
            {
                WayPoint prevPoint = m_points[i - 1];
                WayPoint thisPoint = m_points[i];
                Gizmos.DrawLine(prevPoint.transform.position, thisPoint.transform.position);
            }
        }
    }
}
