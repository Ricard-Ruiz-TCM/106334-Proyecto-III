﻿using UnityEngine;

[CreateAssetMenu(fileName = "new Attack", menuName = "Combat/Skills/Attack")]
public class Attack : Skill {

    public override void Special(Actor from) {
        FindObjectOfType<CombatManager>().Attack(from, range,skill);

    }

}
