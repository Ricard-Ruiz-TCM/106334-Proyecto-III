using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Cleave", menuName = "Combat/Cleave")]
public class Cleave : Skill {

    public override void Special() {
        Debug.Log("Cleave special attack");
    }

}
