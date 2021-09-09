using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BattlePreparationManager : MonoBehaviour
{
    [SerializeField] private HeroData _heroData;

    [Serializable]
    class HeroAttributesItems
    {
        public Image HeroImage;
        public Text HeroNameText;
        public Text HeroLevelText;
        public Text HeroXPText;
        public Text HeroHPText;
        public Text HeroAPText;
    }
    [SerializeField] private HeroAttributesItems _heroAttributesItems;

    [Serializable]
    class HeroPreviewItems
    {
        public Image HeroSelectionBackground;
        public Button HeroPreviewButton;
        public Image HeroImage;
        public Text HeroNameText;
    }
    [SerializeField] private HeroPreviewItems[] _heroPreviewItems;

    private void OnEnable()
    {
        EventManager.SelectHeroForBattle += FillHeroAttributesPanel;
    }
    private void OnDisable()
    {
        EventManager.SelectHeroForBattle -= FillHeroAttributesPanel;
    }
    private void Start()
    {
        FillGeneralHeroPanel();
        FillHeroAttributesPanel(0);//set first hero as default
        SetSelectedHeroBackGround();
    }
    private void FillHeroAttributesPanel(int HeroIndex)
    {
        #region Fill attributes panel with hero's datas
        _heroAttributesItems.HeroImage.sprite = _heroData.Heroes[HeroIndex].HeroImage;
        _heroAttributesItems.HeroNameText.text = _heroData.Heroes[HeroIndex].Name;
        _heroAttributesItems.HeroLevelText.text = _heroData.Heroes[HeroIndex].Level.ToString();
        _heroAttributesItems.HeroXPText.text = _heroData.Heroes[HeroIndex].Experince.ToString();
        _heroAttributesItems.HeroHPText.text = _heroData.Heroes[HeroIndex].Health.ToString();
        _heroAttributesItems.HeroAPText.text = _heroData.Heroes[HeroIndex].AttackPower.ToString();
        #endregion

        SelectHero(HeroIndex);
    }
    private void FillGeneralHeroPanel()
    {
        #region Fill hero preview panel items according to hero order
        for (int i = 0; i < _heroData.Heroes.Length; i++)
        {
            if (_heroData.Heroes[i].IsUnlocked == true)//fill it if player has this hero
            {
                _heroPreviewItems[i].HeroPreviewButton.GetComponent<Transform>().gameObject.SetActive(true);
                _heroPreviewItems[i].HeroPreviewButton.interactable = true;
                _heroPreviewItems[i].HeroImage.sprite = _heroData.Heroes[i].HeroImage;
                _heroPreviewItems[i].HeroNameText.text = _heroData.Heroes[i].Name;
            }
        }
        #endregion
    }
    private void SelectHero(int HeroIndex)
    {
        for (int i = 0; i < _heroData.SelectedHeroesIndex.Length; i++)
        {
            if (HeroIndex == _heroData.SelectedHeroesIndex[i])//if new selected hero is equal any current hero
                return;
        }

        for (int i = 0; i < _heroData.SelectedHeroesIndex.Length; i++)
        {
            if (i < _heroData.SelectedHeroesIndex.Length - 1) //scroll array 1 element until last element
                _heroData.SelectedHeroesIndex[i] = _heroData.SelectedHeroesIndex[i + 1];
            else
                _heroData.SelectedHeroesIndex[_heroData.SelectedHeroesIndex.Length - 1] = HeroIndex; //set array's last element to new hero index
        }

        SetSelectedHeroBackGround();
    }

    private void SetSelectedHeroBackGround()
    {
        for (int i = 0; i < _heroPreviewItems.Length; i++)
            _heroPreviewItems[i].HeroSelectionBackground.color = Color.gray;

        for (int i = 0; i < _heroData.SelectedHeroesIndex.Length; i++)
            _heroPreviewItems[_heroData.SelectedHeroesIndex[i]].HeroSelectionBackground.color = Color.green;
    }

}
