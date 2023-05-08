using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Talon", menuName = "Combat/Skills/TalonDeAquiles")]
public class TalonDeAquiles : Skill
{
    public override void Special(Actor from)
    {
        from.UpdateAttack(_damageMod, "*",1);
        from.EndAction();
    }
}
