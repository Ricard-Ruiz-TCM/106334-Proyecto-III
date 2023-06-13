using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TargetInfoUI : MonoBehaviour, IPointerExitHandler {

    /** Singleton Instance */
    private static TargetInfoUI _instance;
    public static TargetInfoUI instance {
        get {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<TargetInfoUI>();
            return _instance;
        }
    }

    public Sprite baseSprite;

    [SerializeField]
    private UIText _steps, _defense, _name;
    public Image _imgHealth;
    [SerializeField]
    private Transform _skillContainer, _perkContainer;
    public GameObject _extraInfoPanel;

    public ExtraInfoEquipUI _weaponItem;
    public ExtraInfoEquipUI _armorItem;
    public ExtraInfoEquipUI _shieldItem;

    public void Awake() {
        _instance = this;
        gameObject.SetActive(false);
    }

    public void ShowPanel(Actor target) {

        gameObject.SetActive(true);

        _name.UpdateText(target.gameObject.name);
        _steps.UpdateText(target.maxSteps());
        _defense.UpdateText(target.totalDefense());
        _imgHealth.fillAmount = (float)((float)(target).health() / (float)(target).maxHealth());

        // Skills
        int i = 0;
        foreach (Transform child in _skillContainer) {
            if (i < target.skills.skills.Count) {
                SkillItem skI = target.skills.skills[i];
                child.GetComponent<Image>().sprite = skI.skill.icon;
                child.GetComponent<ExtraInfoHoverActivator>().setKeys(skI.skill.keyName, skI.skill.keyDesc);
                i++;
            } else {
                child.GetComponent<Image>().sprite = baseSprite;
                child.GetComponent<ExtraInfoHoverActivator>().setKeys("", "");
            }
        }

        // Perks
        i = 0;
        foreach (Transform child in _perkContainer) {
            if (i < target.perks.perks.Count) {
                PerkItem pkI = target.perks.perks[i];
                child.GetComponent<Image>().sprite = pkI.perk.icon;
                child.GetComponent<ExtraInfoHoverActivator>().setKeys(pkI.perk.keyName, pkI.perk.keyDesc);

                i++;
            } else {
                child.GetComponent<Image>().sprite = baseSprite;
                child.GetComponent<ExtraInfoHoverActivator>().setKeys("", "");
            }
        }

        // Equipment

        _weaponItem.clear();
        _armorItem.clear();
        _shieldItem.clear();

        if (target.equip.weapon != null) {
            _weaponItem.setItem(target.equip.weapon.icon, target.equip.weapon.keyName, target.equip.damage, false);
            _weaponItem.gameObject.GetComponent<ExtraInfoHoverActivator>().setKeys(target.equip.weapon.keyName, target.equip.weapon.keyDescription);
        }
        if (target.equip.armor != null) {
            _armorItem.setItem(target.equip.armor.icon, target.equip.armor.keyName, target.equip.armorDefense, false);
            _armorItem.gameObject.GetComponent<ExtraInfoHoverActivator>().setKeys(target.equip.armor.keyName, target.equip.armor.keyDescription);
        }
        if (target.equip.shield != null) {
            _shieldItem.setItem(target.equip.shield.icon, target.equip.shield.keyName, target.equip.shieldDefense, false);
            _shieldItem.gameObject.GetComponent<ExtraInfoHoverActivator>().setKeys(target.equip.shield.keyName, target.equip.shield.keyDescription);
        }

    }

    private void Update() {
        if (uCore.Action.GetKeyDown(KeyCode.Escape)) {
            HidePanel();
        }
        if (Input.GetMouseButton(0)) {
            HidePanel();
        }
        if (Input.GetMouseButton(1)) {
            HidePanel();
        }
        if (Input.GetMouseButton(2)) {
            HidePanel();
        }
    }

    public void HidePanel() {
        gameObject.SetActive(false);
        _extraInfoPanel.SetActive(false);
        Stage.StageBuilder.clearGrid();
    }

    public void OnPointerExit(PointerEventData eventData) {
        HidePanel();
    }
}
