using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Node", menuName = "Dialogue/DialogueNode", order = 0)]
public class DialogNode : ScriptableObject {

    [Header("Speaker:")]
    public string keyName;
    public string keyMessage;

    [Header("Next Node:")]
    public DialogNode nextNode;

    [Header("Player Options")]
    public List<DialogOption> options;

    [Header("Speaker:")]
    public Sprite speaker;

}
