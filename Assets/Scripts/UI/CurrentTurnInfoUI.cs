using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentTurnInfoUI : MonoBehaviour
{

    [SerializeField]
    private UIText _health, _defense;

    [SerializeField]
    private GameObject _longStatusUIpfb;

    [SerializeField]
    private Transform _longStatusContainer;

    public void UpdatePanel(Actor actor) {
        _health.UpdateText(actor.GetHealth());
        _defense.UpdateText(actor.Defense());

        ClearLongStatus();
        foreach (StatusItem si in actor.Status.ActiveStatus) {
            GameObject.Instantiate(_longStatusUIpfb, _longStatusContainer).GetComponent<LongStatusPanelUI>().UpdateStatus(si);
        }

    }

    public void ClearLongStatus() {
        foreach (Transform child in _longStatusContainer) {
            GameObject.Destroy(child.gameObject);
        }
    }

}
