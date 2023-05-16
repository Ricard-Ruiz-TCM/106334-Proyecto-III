using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ImperialCry", menuName = "Combat/Skills/Imperial Cry")]
public class ImperialCry : Skill {
    
    // Observer para avisar del ImperialCry
    public static event Action<buffsID> onImperialCry;

    public override void Action(Turnable from) {
        base.Action(from);
        onImperialCry?.Invoke(buffsID.Motivated);
    }

}
