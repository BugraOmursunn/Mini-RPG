using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TownPatrolManager : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private Transform targetWaypoint;
    private NavMeshAgent _thisAgent;

    private void Start()
    {
        _thisAgent = GetComponent<NavMeshAgent>();
        GetWaypoints();
    }
    private void GetWaypoints()
    {
        _waypoints = EventManager.waypoints;
    }

    private void Update()
    {
        if (_thisAgent.remainingDistance < 0.5f && _waypoints.Length > 0)
        {
            targetWaypoint = _waypoints[Random.Range(0, _waypoints.Length)];
            _thisAgent.SetDestination(targetWaypoint.position);
        }
    }
}
