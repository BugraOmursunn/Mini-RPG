using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class HeroManager : MonoBehaviour
{
    [SerializeField] private HeroData _heroData;
    public int HeroIndex;
    [SerializeField] private GameObject SelectionArrow;
    [SerializeField] private float HoldTime;
    [SerializeField] private bool IsMouseHolding;

    [SerializeField] private Text NameText;
    [SerializeField] private int CurrentHealt;
    [SerializeField] private Image HealtFillImage;
    [SerializeField] private GameObject DamageTextMesh;
    private void OnEnable()
    {
        EventManager.SelectedHeroIndex += OpenSelectionArrow;
    }
    private void OnDisable()
    {
        EventManager.SelectedHeroIndex -= OpenSelectionArrow;
    }
    private void OnMouseDown()
    {
        if (EventManager.AttackInProgress == false && EventManager.BattleTurn == 0)//if attack action is not active, if heroes turn
        {
            EventManager.SelectedHeroIndex.Invoke(HeroIndex);
            EventManager.IsHeroSelected = true;
            IsMouseHolding = true;
        }
    }
    private void OnMouseUp()
    {
        if (EventManager.AttackInProgress == false && EventManager.BattleTurn == 0)//if attack action is not active, if heroes turn
        {
            IsMouseHolding = false;
            HoldTime = 0;
        }
    }
    private void Start()
    {
        CurrentHealt = _heroData.Heroes[HeroIndex].Health;
        NameText.text = _heroData.Heroes[HeroIndex].Name;
    }
    private void Update()
    {
        if (IsMouseHolding == true && HoldTime < 3)
            HoldTime += Time.deltaTime;

        if (HoldTime >= 3 && EventManager.IsInfoPanelOpen == false)
        {
            EventManager.OpenInfoPanel.Invoke(HeroIndex);
            EventManager.IsInfoPanelOpen = false;
        }
    }
    private void OpenSelectionArrow(int SelectedHeroIndex)
    {
        if (HeroIndex == SelectedHeroIndex)//if selected hero index is this hero index
            SelectionArrow.SetActive(true);
        else
            SelectionArrow.SetActive(false);
    }
    public void GetHit(int DamageValue)
    {
        CurrentHealt = CurrentHealt - DamageValue;
        HealtFillImage.DOFillAmount(((100f / _heroData.Heroes[HeroIndex].Health) * CurrentHealt) / 100f, 0.1f);// example ((100/150) * 50) / 100 
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
        EventManager.HeroDied.Invoke(HeroIndex);
        Destroy(this.gameObject, 1);
    }

}
