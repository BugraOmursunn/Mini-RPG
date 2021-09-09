using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BattleManager : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private HeroData _heroData;
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private Transform[] HeroSpawnPositions;
    [SerializeField] private Transform[] EnemySpawnPositions;

    [SerializeField] private HeroManager[] HeroManagers;//hero managers in Scene
    [SerializeField] private EnemyManager EnemyManager; //enemy manager in Scene

    [SerializeField] private int SelectedHeroIndex, SpawnedEnemyIndex;

    [SerializeField] private int AliveHeroValue, AliveEnemyValue;
    [SerializeField] private bool IsGameEnd;
    private void OnEnable()
    {
        EventManager.DoAttack += DoHeroAttack;
        EventManager.SelectedHeroIndex += GetHeroIndex;
        EventManager.HeroDied += HeroDied;
        EventManager.EnemyDied += EnemyDied;
    }
    private void OnDisable()
    {
        EventManager.DoAttack -= DoHeroAttack;
        EventManager.SelectedHeroIndex -= GetHeroIndex;
        EventManager.HeroDied -= HeroDied;
        EventManager.EnemyDied -= EnemyDied;
    }
    private void Start()
    {
        HeroManagers = new HeroManager[3];
        HeroSpawnPositions = EventManager.HeroSpawnPoints;
        EnemySpawnPositions = EventManager.EnemySpawnPoints;
        SpawnHeroes();
        SpawnEnemies();
    }
    private void SpawnHeroes()
    {
        for (int i = 0; i < 3; i++)//spwan our heroes
        {
            GameObject Hero = Instantiate(_heroData.Heroes[_heroData.SelectedHeroesIndex[i]].HeroPrefab, HeroSpawnPositions[i].position, HeroSpawnPositions[i].rotation);
            HeroManagers[i] = Hero.GetComponent<HeroManager>();
        }
    }
    private void SpawnEnemies()
    {
        SpawnedEnemyIndex = Random.Range(0, 3);
        GameObject Enemy = Instantiate(_enemyData.Enemies[SpawnedEnemyIndex].EnemyPrefab, EnemySpawnPositions[0].position, EnemySpawnPositions[0].rotation);//select random enemy from enemy poll
        EnemyManager = Enemy.GetComponent<EnemyManager>();
    }
    private void GetHeroIndex(int _heroIndex)
    {
        SelectedHeroIndex = _heroIndex;
    }

    private void DoHeroAttack()
    {
        if (IsGameEnd == true)
            return;

        for (int i = 0; i < HeroManagers.Length; i++)//match selected hero's hero index
        {
            if (HeroManagers[i].HeroIndex == SelectedHeroIndex)
                HeroManagers[i].GiveHit();
        }

        EnemyManager.GetHit(_heroData.Heroes[SelectedHeroIndex].AttackPower);//sent selected hero's attack power to enemy get hit func

        #region EventManager
        EventManager.BattleTurn = 1;
        EventManager.IsHeroSelected = false;
        EventManager.SelectedHeroIndex.Invoke(-1);//-1 for clear all selection arrows
        EventManager.AttackInProgress = true;
        #endregion

        Invoke("DoEnemyAttack", 2);
    }
    private void DoEnemyAttack()
    {
        if (IsGameEnd == true)
            return;

        EnemyManager.GiveHit();
        HeroManagers[Random.Range(0, HeroManagers.Length)].GetHit(_enemyData.Enemies[SpawnedEnemyIndex].AttackPower);

        #region EventManager
        EventManager.BattleTurn = 0;
        EventManager.IsHeroSelected = false;
        EventManager.SelectedHeroIndex.Invoke(-1);//-1 for clear all selection arrows
        EventManager.AttackInProgress = false;
        #endregion
    }
    private void HeroDied(int DiedHeroIndex)
    {
        for (int i = 0; i < HeroManagers.Length; i++)
        {
            if (HeroManagers[i].HeroIndex == DiedHeroIndex)//match hero manager's hero index with hero data hero index
            {
                List<HeroManager> HeroManagerList = HeroManagers.ToList();//remove died hero from array
                HeroManagerList.Remove(HeroManagers[i]);
                HeroManagers = HeroManagerList.ToArray();
            }
        }

        if (HeroManagers.Length == 0)//game over
        {
            GameFinished(false);
        }
    }
    private void EnemyDied()
    {
        GameFinished(true);
    }
    private void GameFinished(bool IsWin)
    {
        IsGameEnd = true;
        _gameData.PlayedBattleCount++;

        if (IsWin == true)
        {
            //open win panel
            CheckHeroesSituations();
        }
        else
        {
            //there is nothing to do with lose
        }

        OpenRandomHero();
        EventManager.SaveProgress.Invoke();
        EventManager.GameFinished.Invoke(IsWin);
    }

    private void CheckHeroesSituations()
    {
        for (int i = 0; i < HeroManagers.Length; i++)
            _heroData.Heroes[HeroManagers[i].HeroIndex].Experince++;//give every alive hero +1 xp


        for (int i = 0; i < HeroManagers.Length; i++)
        {
            if (_heroData.Heroes[HeroManagers[i].HeroIndex].Experince != 0  //if hero xp diffrent than 0
                && _heroData.Heroes[HeroManagers[i].HeroIndex].Experince % 5 == 0) //if hero has 5 xp
            {
                _heroData.Heroes[HeroManagers[i].HeroIndex].Level++;//give level for every 5 xp
                _heroData.Heroes[HeroManagers[i].HeroIndex].Experince = 0;

                _heroData.Heroes[HeroManagers[i].HeroIndex].Health += _heroData.Heroes[HeroManagers[i].HeroIndex].Health / 10;
                _heroData.Heroes[HeroManagers[i].HeroIndex].AttackPower += _heroData.Heroes[HeroManagers[i].HeroIndex].AttackPower / 10;
            }
        }
    }

    [SerializeField] private List<int> LockedHeroIndexes;
    private void OpenRandomHero()
    {
        if (_gameData.PlayedBattleCount != 0 && _gameData.PlayedBattleCount % _gameData.HeroPerBattle == 0 //if player played 5 game
            && _heroData.CurrentHeroNumber < _heroData.MaxHeroNumber)                //if player not reach max hero number yet
        {
            _gameData.PlayedBattleCount = 0;
            for (int i = 0; i < _heroData.Heroes.Length; i++)
            {
                if (_heroData.Heroes[i].IsUnlocked == false)//find locked heroes
                    LockedHeroIndexes.Add(i);   //add to list
            }

            int random = Random.Range(0, LockedHeroIndexes.Count);
            _heroData.Heroes[LockedHeroIndexes[random]].IsUnlocked = true; //get random index's value

            _heroData.CurrentHeroNumber++;
        }
    }
}
