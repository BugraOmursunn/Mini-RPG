using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private HeroData _heroData;
    [SerializeField] private EnemyData _enemyData;
    private void OnEnable()
    {
        EventManager.OpenBattleScene += OpenBattleScene;
        EventManager.OpenTownScene += OpenTownScene;
        EventManager.ExitGame += ExitGame;
        EventManager.SaveProgress += SaveDatas;
    }
    private void OnDisable()
    {
        EventManager.OpenBattleScene -= OpenBattleScene;
        EventManager.OpenTownScene -= OpenTownScene;
        EventManager.ExitGame -= ExitGame;
        EventManager.SaveProgress -= SaveDatas;
    }
    private void Awake()
    {
        Application.targetFrameRate = 60;
        SaveManager.LoadGameData(_gameData);
        SaveManager.LoadHeroData(_heroData);
        SaveManager.LoadEnemyData(_enemyData);

        ResetEventManager(); //reset static variables in event manager
    }
    private void SaveDatas()
    {
        SaveManager.SaveGameData(_gameData);
        SaveManager.SaveHeroData(_heroData);
        SaveManager.SaveEnemyData(_enemyData);
    }
    private void OpenBattleScene()
    {
        SaveDatas();
        SceneManager.LoadScene("BattleScene");
    }
    private void OpenTownScene()
    {
        SceneManager.LoadScene("TownScene");
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void ResetEventManager()
    {
        EventManager.ChangePanelStatus?.Invoke(0);
        EventManager.FillHeroInspectPanel?.Invoke(0);
        EventManager.SelectHeroForBattle?.Invoke(0);

        EventManager.BattleTurn = 0;
        EventManager.IsHeroSelected = false;
        EventManager.SelectedHeroIndex?.Invoke(-1);//-1 for clear all selection arrows
        EventManager.AttackInProgress = false;
    }
}
