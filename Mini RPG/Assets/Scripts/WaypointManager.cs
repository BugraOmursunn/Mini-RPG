using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    [SerializeField] private Transform[] Waypoints;

    private void Awake()
    {
        Waypoints = new Transform[this.transform.childCount];
        SetWaypoints();
    }
    private void SetWaypoints()
    {
        for (int i = 0; i < this.transform.childCount; i++)
            Waypoints[i] = this.transform.GetChild(i).GetComponent<Transform>();

        EventManager.waypoints = Waypoints;
    }
}
