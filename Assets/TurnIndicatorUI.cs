using UnityEngine;

public class TurnIndicatorUI : MonoBehaviour {

    public bool active;
    public RectTransform targetRectTransform;

    public Vector3 offset;

    private void OnEnable() {
        TurnManager.instance.onStartSystem += () => { active = true; };
    }

    private void OnDisable() {
        TurnManager.instance.onStartSystem -= () => { active = true; };
    }

    void Update() {
        if (!active)
            return;

        Vector3 position = TurnManager.instance.current.transform.position;

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(position);
        targetRectTransform.position = screenPoint + offset;
    }

}
