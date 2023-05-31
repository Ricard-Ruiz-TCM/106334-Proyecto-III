using UnityEngine;
using UnityEngine.UI;

public class PerkSelectorUI : MonoBehaviour {


    /** Tenemos la perk seleciconada */
    private bool _selected = false;

    [SerializeField]
    private PerkSelectorUI _other;

    [SerializeField, Header("Perk:")]
    private Perk _perk;

    [SerializeField]
    private UIText _txtName, _txtDesc;

    [SerializeField, Header("SelectButton:")]
    private Button _btn;

    public void setPerk(Perk perk) {
        _perk = perk;
        updateTexts();
    }

    public void updateTexts() {
        _txtName.SetKey(_perk.keyName);
        _txtDesc.SetKey(_perk.keyDesc);
    }

    public void BTN_SelectPerk() {
        _btn.interactable = true;
        select();
        _other.deSelect();
    }

    public void select() {
        _selected = true;
    }

    public void deSelect() {
        _selected = false;
    }

    public void BTN_IsSelected() {
        if (_selected) {
            uCore.GameManager.getPlayer().perks.addPerk(_perk.ID);
        }
    }

}
