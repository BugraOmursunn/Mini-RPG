using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData_ScriptableObject", order = 3)]
public class EnemyData : ScriptableObject
{
    [Serializable]
    public class EnemyAttributes
    {
        public GameObject EnemyPrefab;
        public string Name;
        public int Health;
        public int AttackPower;
    }

    [Header("This is enemy attributes")]
    public EnemyAttributes[] Enemies;
}
