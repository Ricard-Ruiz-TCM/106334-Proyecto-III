using System.Collections.Generic;
using UnityEngine;

public class TutorialUIManager : MonoBehaviour {
    [SerializeField]
    private GameObject firstPanel;
    [SerializeField]
    private List<GameObject> firstRoundOfPanels;
    [SerializeField]
    private GameObject positioningPanel;
    [SerializeField]
    private List<GameObject> positioningRoundOfPanels;
    [SerializeField]
    private GameObject combatPanel;

    private bool firstTimeInCombat = true;

    void OnEnable() {
        TurnManager.onNewRound += ActivatePanel;
        //DialogManager.instance.onNextDialog += UpdateDialog;

        Stage.onCompleteStage += (stageResolution res) => {
            if (firstPanel != null)
                firstPanel.SetActive(false);
            if (combatPanel != null)
                combatPanel.SetActive(false);
        };
    }

    // Unity OnDisable
    void OnDisable() {
        TurnManager.onNewRound -= ActivatePanel;

        Stage.onCompleteStage -= (stageResolution res) => {
            if (firstPanel != null)
                firstPanel.SetActive(false);
            if (combatPanel != null)
                combatPanel.SetActive(false);
        };
    }

    public void ActivatePanel(roundType roundType) {
        switch (roundType) {
            case roundType.positioning:
                firstPanel.SetActive(true);
                firstPanel.GetComponent<TutorialUI>().setRoundsHaveStarted();
                break;
            case roundType.combat:
                if (firstTimeInCombat) {
                    if(positioningRoundOfPanels != null)
                    {
                        foreach (var positionRoundPanel in positioningRoundOfPanels)
                        {
                            positionRoundPanel.SetActive(false);
                        }
                    }                    
                    firstTimeInCombat = false;
                    combatPanel.SetActive(true);
                    combatPanel.GetComponent<TutorialUI>().setRoundsHaveStarted();
                }
                break;
            default:
                break;
        }
    }

    public void ActivateOnPositioningPanel() {
        foreach (var panel in firstRoundOfPanels) {
            panel.SetActive(false);
        }
        if(positioningPanel != null)
        {
            positioningPanel.SetActive(true);
            positioningPanel.GetComponent<TutorialUI>().setRoundsHaveStarted();
        }        
    }
}
