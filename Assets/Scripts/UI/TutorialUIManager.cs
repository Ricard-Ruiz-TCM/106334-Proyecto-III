using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject firstPanel;
    [SerializeField]
    private List<GameObject> firstRoundOfPanels;
    [SerializeField]
    private GameObject positioningPanel;
    [SerializeField]
    private GameObject combatPanel;

    private bool firstTimeInCombat = true;

    void OnEnable()
    {
        TurnManager.instance.onNewRound += ActivatePanel;
        //DialogManager.instance.onNextDialog += UpdateDialog;
    }

    // Unity OnDisable
    void OnDisable()
    {
        TurnManager.instance.onNewRound -= ActivatePanel;
    }

    public void ActivatePanel(roundType roundType)
    {
        switch(roundType)
        {
            case roundType.positioning:
                firstPanel.SetActive(true);
                firstPanel.GetComponent<TutorialUI>().setRoundsHaveStarted();
                break;
            case roundType.combat:
                if(firstTimeInCombat)
                {
                    firstTimeInCombat = false;
                    combatPanel.SetActive(true);
                    combatPanel.GetComponent<TutorialUI>().setRoundsHaveStarted();
                }                
                break;
            default:
                break;
        }        
    }

    public void ActivateOnPositioningPanel()
    {
        foreach(var panel in firstRoundOfPanels)
        {
            panel.SetActive(false);
        }
        positioningPanel.SetActive(true);
        positioningPanel.GetComponent<TutorialUI>().setRoundsHaveStarted();
    }
}
