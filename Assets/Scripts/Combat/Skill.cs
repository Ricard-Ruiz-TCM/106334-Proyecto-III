using UnityEngine;

[CreateAssetMenu(fileName = "new Skill", menuName = "Combat/Skills/Skill")]
public class Skill : ScriptableObject {

    [Header("Sprite Icon:")]
    public Sprite icon;

    [Header("Localization Keys:")]
    public string keyName;
    public string keyDesc;

    [Header("Data:")]
    public skillID ID;
    public int cooldown;

    public virtual void Action(Turnable from) {
        CombatManager.instance.UseSkill(from, ID, from.canInteract);
        from.EndAction();
    }

}
