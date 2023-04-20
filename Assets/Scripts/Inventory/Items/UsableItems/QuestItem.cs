using UnityEngine;

[CreateAssetMenu(fileName = "new QuestItem", menuName = "Items/Quest Item")]
public class QuestItem : UsableItem {

    public override void Use() {
        Debug.Log("QuestItem USE");
    }

}
