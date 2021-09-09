using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager
{
    public static void LoadGameData(GameData GameData)
    {
        if (!PlayerPrefs.HasKey("GameData"))
        {
            SaveGameData(GameData);
            return;
        }

        string dataString = PlayerPrefs.GetString("GameData");
        JsonUtility.FromJsonOverwrite(dataString, GameData);
    }
    public static void SaveGameData(GameData GameData)
    {
        string dataString = JsonUtility.ToJson(GameData);
        PlayerPrefs.SetString("GameData", dataString);
    }

    public static void LoadHeroData(HeroData HeroData)
    {
        if (!PlayerPrefs.HasKey("HeroData"))
        {
            SaveHeroData(HeroData);
            return;
        }

        string dataString = PlayerPrefs.GetString("HeroData");
        JsonUtility.FromJsonOverwrite(dataString, HeroData);
    }
    public static void SaveHeroData(HeroData HeroData)
    {
        string dataString = JsonUtility.ToJson(HeroData);
        PlayerPrefs.SetString("HeroData", dataString);
    }

    public static void LoadEnemyData(EnemyData EnemyData)
    {
        if (!PlayerPrefs.HasKey("EnemyData"))
        {
            SaveEnemyData(EnemyData);
            return;
        }

        string dataString = PlayerPrefs.GetString("EnemyData");
        JsonUtility.FromJsonOverwrite(dataString, EnemyData);
    }
    public static void SaveEnemyData(EnemyData EnemyData)
    {
        string dataString = JsonUtility.ToJson(EnemyData);
        PlayerPrefs.SetString("EnemyData", dataString);
    }
}