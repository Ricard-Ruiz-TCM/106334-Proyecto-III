using UnityEngine;

[CreateAssetMenu(fileName = "new Buff", menuName = "Combat/Buffs/Buff")]
public abstract class Buff : ScriptableObject {

    [Header("Sprite Icon:")]
    public Sprite icon;

    [Header("Localization Keys:")]
    public string keyName;
    public string keyDesc;

    [Header("Data:")]
    public buffsID ID;
    public int duration;

    public abstract void onApply(Turnable me);

    public abstract void startTurnEffect(Turnable me);
    public abstract void endTurnEffect(Turnable me);

}
