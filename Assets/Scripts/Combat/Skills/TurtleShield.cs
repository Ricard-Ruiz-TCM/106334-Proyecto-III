using UnityEngine;

[CreateAssetMenu(fileName = "new TurtleShield", menuName = "Combat/Skills/Turtle Shield")]
public class TurtleShield : Skill {

    public override void Special(Actor from) {
        Debug.Log("TurtleShield attack");
    }

}
