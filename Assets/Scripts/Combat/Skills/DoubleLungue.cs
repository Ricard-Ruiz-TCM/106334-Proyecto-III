using UnityEngine;

[CreateAssetMenu(fileName = "Double Lungue", menuName = "Combat/Skills/Double Lungue")]
public class DoubleLungue : Skill {

    public override void action(BasicActor from, Node to) {
        // TODO
        from.endAction();
    }

}
