using UnityEngine;

public class CameraPivot : MonoBehaviour {

    public bool _active = false;

    private void OnEnable() {
        TurnManager.onStartSystem += () => { _active = true; };
    }

    private void OnDisable() {
        TurnManager.onStartSystem -= () => { _active = true; };
    }

    // Update is called once per frame
    void Update() {
        if (!_active)
            return;

        if (TurnManager.instance.attenders.Count <= 0)
            return;

        Vector3 centerPoint = TurnManager.instance.attenders[0].transform.position;
        for (int i = 1; i < TurnManager.instance.attenders.Count; i++) {
            centerPoint += TurnManager.instance.attenders[i].transform.position;
        }

        centerPoint /= TurnManager.instance.attenders.Count;

        if (TurnManager.instance.current is ManualActor) {
            Vector3 playerPoint = TurnManager.instance.current.transform.position;


        }

        transform.position = centerPoint;
    }

}
