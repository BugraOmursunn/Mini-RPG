using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    //Have to reset these static elements. They are not resetting with scene change #DO THIS# 

    #region Town scene elements

    public static Transform[] waypoints;            //in town scene NavMesh patrol waypoints


    public static Action<int> ChangePanelStatus;    //UI panel control action, get panel index

    public static Action<int> FillHeroInspectPanel; //get hero index
    public static Action<int> SelectHeroForBattle;  //get hero index

    public static Action OpenBattleScene;

    #endregion



    #region Battle scene elements

    public static Action OpenTownScene;
    public static Transform[] HeroSpawnPoints;//get heroes spawn points' transforms in battle scene
    public static Transform[] EnemySpawnPoints;//get enemies spawn points' transforms in battle scene

    public static Action<int> SelectedHeroIndex;//get hero index, -1 for clear 
    public static bool IsHeroSelected;//check if hero selected or not

    public static Action<int> OpenInfoPanel;//get hero index
    public static bool IsInfoPanelOpen;//check if info panel open or not
    public static Action CloseInfoPanel;


    public static Action DoAttack;//get hero index, get enemy index
    public static bool AttackInProgress;//check if attack action is active or not

    public static int BattleTurn;//0 for heroes, 1 for enemy

    public static Action<int> HeroDied;//get died hero index
    public static Action EnemyDied;

    public static Action SaveProgress;

    public static Action<bool> GameFinished;
    #endregion


    public static Action ExitGame;
}
