using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData_ScriptableObject", order = 1)]

//this won't be in demo but if i had to, i can also hold current gold, silver, wood storage or town level etc. datas in this scriptable object 
public class GameData : ScriptableObject
{
    public int HeroPerBattle;//player get new here in every X battle
    public int PlayedBattleCount;
}
