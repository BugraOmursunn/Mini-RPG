using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    [SerializeField] private Transform[] Points;
    [SerializeField] private SpawnPointTypes _spawnPointType;
    private void Awake()
    {
        Points = new Transform[this.transform.childCount];
        SetWaypoints();
    }
    private void SetWaypoints()
    {
        for (int i = 0; i < this.transform.childCount; i++)
            Points[i] = this.transform.GetChild(i).GetComponent<Transform>();

        switch (_spawnPointType)
        {
            case SpawnPointTypes.HeroSpawnPoint:
                EventManager.HeroSpawnPoints = Points;
                break;
            case SpawnPointTypes.EnemySpawnPoint:
                EventManager.EnemySpawnPoints = Points;
                break;
        }
    }
}
