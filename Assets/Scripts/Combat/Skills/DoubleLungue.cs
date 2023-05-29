using UnityEngine;

[CreateAssetMenu(fileName = "Double Lungue", menuName = "Combat/Skills/Double Lungue")]
public class DoubleLungue : Skill {

    public override void action(BasicActor from, BasicActor to) {
        // TODO
        from.endAction();
    }

}
