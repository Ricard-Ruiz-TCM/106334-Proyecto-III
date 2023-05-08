using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Tortuga", menuName = "Combat/Skills/FormacionTortuga")]
public class FormacionTortuga : Skill
{
    public override void Special(Actor from)
    {
        from.Turtle();
        from.UpdateDefense(_defenseMod, "+", 1);
        from.EndAction();
    }
}
