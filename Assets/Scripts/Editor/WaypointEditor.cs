using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class WaypointEditor : MonoBehaviour
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmo(WayPoint waypoint, GizmoType gizmoType)
    {
        DrawArrow(waypoint.transform.position, waypoint.transform.forward, 0.5f);

        if ((gizmoType & GizmoType.Selected) != 0)
        {
            Gizmos.color = Color.yellow;
        }
        else
        {
            Gizmos.color = Color.blue;
        }

        if (waypoint.previousWayPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(waypoint.transform.position, waypoint.previousWayPoint.transform.position);
        }
        if (waypoint.nextWayPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(waypoint.transform.position, waypoint.nextWayPoint.transform.position);
        }
        Gizmos.DrawSphere(waypoint.transform.position, 0.1f);

        if (waypoint.branches != null)
        {
            foreach (WayPoint way in waypoint.branches)
            {
                if (way)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(waypoint.transform.position, way.transform.position);
                }
            }
        }
    }

    private static void DrawArrow(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
    {
        Gizmos.DrawRay(pos, direction);

        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
        Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
        Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
    }
}
