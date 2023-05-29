using UnityEngine;

[CreateAssetMenu(fileName = "Disarm", menuName = "Combat/Skills/Disarm")]
public class Disarm : Skill {

    public override void action(BasicActor from, Node to) {
        // TODO
        from.endAction();
    }

}
