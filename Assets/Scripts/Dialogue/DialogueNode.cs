using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Node", menuName = "Dialog", order = 0)]
public class DialogueNode : ScriptableObject {

    public string _textKeyName;
    public string _textKeyMessage;
    public DialogueNode _next;

    public bool Options = false;
    public List<DialogueOption> _options;

    public bool Trigger = false;
    public DialogueTrigger _trigger;

    public bool EndNode = false;
}
