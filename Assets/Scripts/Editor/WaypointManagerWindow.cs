using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WaypointManagerWindow : UnityEditor.EditorWindow
{
    [MenuItem("Window/Waypoint")]
    public static void Open()
    {
        GetWindow<WaypointManagerWindow>();
    }
    public Transform wayPointRoot;

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("wayPointRoot"));
        if (wayPointRoot == null)
        {
            EditorGUILayout.HelpBox("Root is null", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical();
            DrawButtons();
            EditorGUILayout.BeginVertical();
        }

        obj.ApplyModifiedProperties();
    }

    private void DrawButtons()
    {
        if (GUILayout.Button("Create Waypoint"))
        {
            CreateWayPoint();
        }

        if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<WayPoint>())
        {
            if (GUILayout.Button("Create Waypoint Before"))
            {
                AddBefore();
            }
            if (GUILayout.Button("Create Waypoint After"))
            {
                AddAfter();
            }
            if (GUILayout.Button("Remove Waypoint"))
            {
                Remove();
            }
            if (GUILayout.Button("Add Branch"))
            {
                AddBranch();
            }
        }
    }

    private void AddBranch()
    {
        GameObject waypointObject = new GameObject("Waypoint" + wayPointRoot.childCount, typeof(WayPoint));
        waypointObject.transform.SetParent(wayPointRoot, false);
        WayPoint newWayPoint = waypointObject.GetComponent<WayPoint>();
        WayPoint branchedFrom = Selection.activeGameObject.GetComponent<WayPoint>();
        branchedFrom.branches.Add(newWayPoint);

        newWayPoint.transform.position = branchedFrom.transform.position;
        newWayPoint.transform.forward = branchedFrom.transform.forward;
        Selection.activeGameObject = newWayPoint.gameObject;
    }

    private void AddBefore()
    {
        GameObject waypointObject = new GameObject("Waypoint" + wayPointRoot.childCount, typeof(WayPoint));
        waypointObject.transform.SetParent(wayPointRoot, false);

        WayPoint newWayPoint = waypointObject.GetComponent<WayPoint>();

        WayPoint selected = Selection.activeGameObject.GetComponent<WayPoint>();

        waypointObject.transform.position = selected.transform.position;
        waypointObject.transform.forward = selected.transform.forward;

        if (selected.previousWayPoint != null)
        {
            newWayPoint.previousWayPoint = selected.previousWayPoint;
            selected.previousWayPoint.nextWayPoint = newWayPoint;
        }
        newWayPoint.nextWayPoint = selected;
        selected.previousWayPoint = newWayPoint;
        newWayPoint.transform.SetSiblingIndex(selected.transform.GetSiblingIndex());
        Selection.activeGameObject = newWayPoint.gameObject;
    }

    private void AddAfter()
    {
        GameObject waypointObject = new GameObject("Waypoint" + wayPointRoot.childCount, typeof(WayPoint));
        waypointObject.transform.SetParent(wayPointRoot, false);

        WayPoint newWayPoint = waypointObject.GetComponent<WayPoint>();

        WayPoint selected = Selection.activeGameObject.GetComponent<WayPoint>();

        waypointObject.transform.position = selected.transform.position;
        waypointObject.transform.forward = selected.transform.forward;

        if (selected.nextWayPoint != null) ;
        {
            selected.nextWayPoint.previousWayPoint = newWayPoint;
            newWayPoint.nextWayPoint = selected.nextWayPoint;
        }
        selected.nextWayPoint = newWayPoint;
        newWayPoint.transform.SetSiblingIndex(selected.transform.GetSiblingIndex());
        Selection.activeGameObject = newWayPoint.gameObject;
    }

    private void Remove()
    {
        WayPoint selected = Selection.activeGameObject.GetComponent<WayPoint>();
        if (selected.nextWayPoint)
        {
            selected.nextWayPoint.previousWayPoint = selected.previousWayPoint;
        }

        if (selected.previousWayPoint)
        {
            selected.previousWayPoint.nextWayPoint = selected.nextWayPoint;
            Selection.activeGameObject = selected.previousWayPoint.gameObject;
        }
        DestroyImmediate(selected.gameObject);

    }

    private void CreateWayPoint()
    {
        GameObject waypoint = new GameObject("Waypoint" + wayPointRoot.childCount, typeof(WayPoint));
        waypoint.transform.SetParent(wayPointRoot, false);

        WayPoint point = waypoint.GetComponent<WayPoint>();
        if (wayPointRoot.childCount > 1)
        {
            point.previousWayPoint = wayPointRoot.GetChild(wayPointRoot.childCount - 2).GetComponent<WayPoint>();
            point.previousWayPoint.nextWayPoint = point;

            point.transform.position = point.previousWayPoint.transform.position;
            point.transform.forward = point.previousWayPoint.transform.forward;
        }
        Selection.activeGameObject = point.gameObject;
    }
}
