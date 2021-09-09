using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private UIButtonTypes _buttonType;

    [Header("Only, if this is hero button")]
    [SerializeField] private int HeroIndex;
    public void ButtonClick()
    {
        //check this button type
        switch (_buttonType)
        {
            case UIButtonTypes.OpenMainMenuPanel:
                EventManager.ChangePanelStatus(0); //0 for main menu panel
                break;
            case UIButtonTypes.OpenInspectHeroesPanel:
                EventManager.ChangePanelStatus(1); //1 for hero inspect panel
                break;
            case UIButtonTypes.OpenBattlePanel:
                EventManager.ChangePanelStatus(2); //2 for battle preparation panel
                break;
            case UIButtonTypes.PreviewSelectedHero:
                EventManager.FillHeroInspectPanel?.Invoke(HeroIndex);
                break;
            case UIButtonTypes.SetSelectedHero:
                EventManager.SelectHeroForBattle?.Invoke(HeroIndex);
                break;
            case UIButtonTypes.OpenBattleScene:
                EventManager.OpenBattleScene?.Invoke();
                break;
            case UIButtonTypes.ExitGame:
                EventManager.ExitGame?.Invoke();
                break;
            case UIButtonTypes.CloseInfoPopup:
                EventManager.CloseInfoPanel?.Invoke();
                break;
            case UIButtonTypes.OpenTownScene:
                EventManager.OpenTownScene?.Invoke();
                break;
        }
    }
}
