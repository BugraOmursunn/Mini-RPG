using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class HeroInspectManager : MonoBehaviour
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
        EventManager.FillHeroInspectPanel += FillHeroAttributesPanel;
    }
    private void OnDisable()
    {
        EventManager.FillHeroInspectPanel -= FillHeroAttributesPanel;
    }
    private void Start()
    {
        FillGeneralHeroPanel();
        FillHeroAttributesPanel(0);//set first hero as default
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

        SetSelectedHeroBackGround(HeroIndex);//set selected hero's background to green
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
    private void SetSelectedHeroBackGround(int HeroIndex)
    {
        for (int i = 0; i < _heroPreviewItems.Length; i++)
            _heroPreviewItems[i].HeroSelectionBackground.color = Color.gray;

        _heroPreviewItems[HeroIndex].HeroSelectionBackground.color = Color.green;
    }
}
