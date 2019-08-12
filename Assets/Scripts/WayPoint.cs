using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public WayPoint previousWayPoint;
    public WayPoint nextWayPoint;

    [SerializeField] private float m_ReachTargetThreshold = 2;                                // proximity to target to consider we 'reached' it, and stop driving.
    [Range(0,5f)] private float width = 2;                           

    public float ReachTargetThreshold { get { return m_ReachTargetThreshold; } }
}
