using UnityEngine;

public class ProtoCanvasInGameHUD : MonoBehaviour {

    [SerializeField]
    private Actor _actor;
    [SerializeField]
    private UIText _text;

    void Awake() {
        _actor = GetComponentInParent<Actor>();
        _text = GetComponentInChildren<UIText>();
    }

    void Update() {
        if (_actor == null || _text == null)
            return;

        transform.LookAt(Camera.main.transform);
        string str = "HP: " + _actor.Health() +
            "\nDef: " + _actor.Defense() +
            "\nW: " + (_actor.Weapon() != null ? _actor.Weapon().item.ToString() : " X ") +
            "\nS: " + (_actor.Shield() != null ? _actor.Shield().item.ToString() : " X ") +
            "\nA: " + (_actor.Armor() != null ? _actor.Armor().item.ToString() : " X ") +
            "";
        _text.UpdateText(str);
    }

}
