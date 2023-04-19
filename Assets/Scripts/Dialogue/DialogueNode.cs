using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Node", menuName = "Dialogue/DialogueNode", order = 0)]
public class DialogueNode : ScriptableObject {

    [Header("Speaker:")]
    public string NameKey;
    public string MessageKey;
    [Header("Next Node:")]
    public DialogueNode Next;

    [Header("Player Options")]
    public List<DialogueOption> Options;

}
