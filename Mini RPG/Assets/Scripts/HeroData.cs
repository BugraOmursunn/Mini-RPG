using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "HeroData", menuName = "ScriptableObjects/HeroData_ScriptableObject", order = 2)]
public class HeroData : ScriptableObject
{
    //Player can't has more heroes than this number
    public int MaxHeroNumber;
    //Player hero hold number
    public int CurrentHeroNumber;

    [Serializable]
    public class HeroAttributes
    {
        public bool IsUnlocked;
        public Sprite HeroImage;
        public GameObject HeroPrefab;
        public string Name;
        public int Level;
        public int Experince;
        public int Health;
        public int AttackPower;
    }

    [Header("This is hero attributes")]
    public HeroAttributes[] Heroes;
    public int[] SelectedHeroesIndex = new int[3];

}
