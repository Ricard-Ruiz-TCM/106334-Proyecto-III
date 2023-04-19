using System;

[Serializable]
public class DialogueTrigger {

    public Action _trigger;

    public void Trigger() {
        _trigger?.Invoke();
    }

}
