using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private int EnemyIndex;

    [SerializeField] private Text NameText;
    [SerializeField] private int CurrentHealt;
    [SerializeField] private Image HealtFillImage;
    [SerializeField] private GameObject DamageTextMesh;
    private void OnMouseDown()
    {
        //if any hero selected already, if attack action is not active, if heroes turn
        if (EventManager.IsHeroSelected == true && EventManager.AttackInProgress == false && EventManager.BattleTurn == 0)
            EventManager.DoAttack.Invoke();
    }
    private void Start()
    {
        CurrentHealt = _enemyData.Enemies[EnemyIndex].Health;
        NameText.text = _enemyData.Enemies[EnemyIndex].Name;
    }
    public void GetHit(int DamageValue)
    {
        CurrentHealt = CurrentHealt - DamageValue;
        HealtFillImage.DOFillAmount(((100f / _enemyData.Enemies[EnemyIndex].Health) * CurrentHealt) / 100f, 0.1f);// example ((100/150) * 50) / 100 
        GetHitAnim(DamageValue);

        if (CurrentHealt <= 0)
            Die();
    }
    public void GiveHit()
    {
        GiveHitAnim();
    }

    private void GetHitAnim(int DamageValue)
    {
        this.transform.DOShakePosition(1, 1, 10, 90);
        DamageTextMesh.GetComponent<TMPro.TextMeshProUGUI>().text = "-" + DamageValue;
        DamageTextMesh.GetComponent<Animator>().SetTrigger("DamageTrigger");
    }
    private void GiveHitAnim()
    {
        this.transform.DOShakePosition(1, 0.5f, 5, 90);
    }

    private void Die()
    {
        EventManager.EnemyDied.Invoke();
        Destroy(this.gameObject, 1);
    }
}
