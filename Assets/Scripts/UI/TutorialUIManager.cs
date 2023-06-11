using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject firstPanel;

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
        firstPanel.SetActive(true);
        firstPanel.GetComponent<TutorialUI>().setRoundsHaveStarted();
    }
}
