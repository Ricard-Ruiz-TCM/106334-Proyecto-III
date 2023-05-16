using UnityEngine;

public class PlayerTurnInfoUI : MonoBehaviour {

    [SerializeField]
    private GameObject _shortStatusUIpfb;

    [SerializeField]
    private Transform _shortStatusContainer;

    public void UpdatePanel(Turnable actor) {
        ClearShortStatus();
        foreach (BuffItem si in actor.Status.ActiveStatus) {
            GameObject.Instantiate(_shortStatusUIpfb, _shortStatusContainer).GetComponent<ShortStatusPanelUI>().UpdateStatus(si);
        }

    }

    public void ClearShortStatus() {
        foreach (Transform child in _shortStatusContainer) {
            GameObject.Destroy(child.gameObject);
        }
    }
}
