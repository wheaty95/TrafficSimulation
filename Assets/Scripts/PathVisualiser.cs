using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathVisualiser : MonoBehaviour
{
    public WayPoint m_start;
    public WayPoint m_end;
    List<WayPoint> points;
    // Start is called before the first frame update
    void Update()
    {
        List<WayPoint> been = new List<WayPoint>();
        points = PathFinder.FindShortestNodePath(m_start, m_end, been);
        if (points == null || points.Count == 0)
        {
            Debug.Log("No Path");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (points != null)
        {
            for (int i = 1; i < points.Count; i++)
            {
                WayPoint prevPoint = points[i - 1];
                WayPoint thisPoint = points[i];
                Gizmos.DrawLine(prevPoint.transform.position, thisPoint.transform.position);
            }
        }
    }
}
