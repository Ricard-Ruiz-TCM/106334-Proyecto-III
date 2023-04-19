using UnityEngine;

[CreateAssetMenu(fileName = "new StartDialogue", menuName = "Dialogue/Custom/StartDialogue")]
public class StartDialogue : DialogueTrigger {

    public override void Trigger() {
        Debug.Log("Trigger");
    }

}
