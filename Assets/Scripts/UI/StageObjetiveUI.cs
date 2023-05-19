using UnityEngine;

public class StageObjetiveUI : MonoBehaviour {

    [SerializeField]
    public UIText txtName;
    [SerializeField]
    public UIText txtObjetive;

    /** Establece la informaicón básica del stage, nombre y objetivo */
    public void SetObjetive(StageData data) {
        txtName.SetKey(data.keyName);
        txtObjetive.SetKey(data.objetive.ToString());
    }

    public void BTN_Read() {
        TurnManager.instance.completeRoundType(roundType.thinking);
        gameObject.SetActive(false);
    }

}