using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MainUIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup[] UIPanels;
    [SerializeField] private int CurrentPanelIndex; //current open panel index
    private void OnEnable()
    {
        EventManager.ChangePanelStatus += ChangePanelStatus;
    }
    private void OnDisable()
    {
        EventManager.ChangePanelStatus -= ChangePanelStatus;
    }

    private void ChangePanelStatus(int PanelIndex)
    {
        //check event manager panel status [0] for main menu,[1] for hero inspector menu,[2] for battle preparation menu {you can find order in EnumHolder.cs}
        switch (PanelIndex)
        {
            case 0:
                if (CurrentPanelIndex == 1)     //if hero inspect active
                    TweenSeq(0, 1);
                else if (CurrentPanelIndex == 2)//if battle preparation active
                    TweenSeq(0, 2);
                break;
            case 1:
                TweenSeq(1, 0);
                break;
            case 2:
                TweenSeq(2, 0);
                break;
        }
        CurrentPanelIndex = PanelIndex;
    }
    private void TweenSeq(int OpenPanelIndex, int ClosePanelIndex)//the first parameter[OpenPanelIndex] is for the opening panel, the second parameter[ClosePanelIndex] for closing panel
    {
        UIPanels[OpenPanelIndex].interactable = true; UIPanels[OpenPanelIndex].blocksRaycasts = true;
        UIPanels[ClosePanelIndex].interactable = false; UIPanels[ClosePanelIndex].blocksRaycasts = false;

        Sequence TweenSeq = DOTween.Sequence();
        TweenSeq.Kill();
        TweenSeq.Append(UIPanels[ClosePanelIndex].DOFade(0, 0.2f));                //close current panel
        TweenSeq.Append(UIPanels[OpenPanelIndex].DOFade(1, 0.2f).SetDelay(0.1f));     //open next panel with delay
        TweenSeq.Play();
    }

}
