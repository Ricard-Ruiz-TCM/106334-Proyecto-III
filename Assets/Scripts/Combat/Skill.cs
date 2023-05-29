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

    public bool personal;

    public int areaRange;

    public virtual void action(BasicActor from, Node to) {
        BasicActor target = Stage.StageManager.getActor(to);
        if (target != null) {
            target.takeDamage((Actor)from, from.totalDamage());
        }
        from.endAction();
    }
}
