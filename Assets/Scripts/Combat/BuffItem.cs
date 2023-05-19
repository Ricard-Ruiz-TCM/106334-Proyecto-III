using System;

[Serializable]
public class BuffItem {

    public Buff buff;
    public int duration;

    public BuffItem(Buff data) {
        buff = data;
        duration = data.duration;
    }

}
