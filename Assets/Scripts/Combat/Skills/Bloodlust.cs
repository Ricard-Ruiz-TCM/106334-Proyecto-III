using UnityEngine;

[CreateAssetMenu(fileName = "Bloodlust", menuName = "Combat/Skills/Bloodlust")]
public class Bloodlust : Skill {

    public override void action(BasicActor from, BasicActor to) {
        // TODO
        from.endAction();
    }

}
