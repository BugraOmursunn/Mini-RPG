using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;
public class GameUIManager : MonoBehaviour
{
    [SerializeField] private HeroData _heroData;
    [SerializeField] private RectTransform InfoPanel;
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
    [SerializeField] private GameObject[] ResultPanels;

    private void OnEnable()
    {
        EventManager.OpenInfoPanel += OpenInfoPanel;
        EventManager.CloseInfoPanel += CloseInfoPanel;
        EventManager.GameFinished += OpenResultPanel;
    }
    private void OnDisable()
    {
        EventManager.OpenInfoPanel -= OpenInfoPanel;
        EventManager.CloseInfoPanel -= CloseInfoPanel;
        EventManager.GameFinished -= OpenResultPanel;
    }
    private void OpenInfoPanel(int HeroIndex)
    {
        InfoPanel.DOScale(0.42f, 0.2f);

        #region Fill info panel with hero's datas
        _heroAttributesItems.HeroImage.sprite = _heroData.Heroes[HeroIndex].HeroImage;
        _heroAttributesItems.HeroNameText.text = _heroData.Heroes[HeroIndex].Name;
        _heroAttributesItems.HeroLevelText.text = _heroData.Heroes[HeroIndex].Level.ToString();
        _heroAttributesItems.HeroXPText.text = _heroData.Heroes[HeroIndex].Experince.ToString();
        _heroAttributesItems.HeroHPText.text = _heroData.Heroes[HeroIndex].Health.ToString();
        _heroAttributesItems.HeroAPText.text = _heroData.Heroes[HeroIndex].AttackPower.ToString();
        #endregion
    }
    private void CloseInfoPanel()
    {
        InfoPanel.DOScale(0, 0.2f);
        EventManager.IsInfoPanelOpen = false;
    }
    private void OpenResultPanel(bool FinishResult)
    {
        if (FinishResult == true)//win
            ResultPanels[0].SetActive(true);
        else//lose
            ResultPanels[1].SetActive(true);
    }
}
