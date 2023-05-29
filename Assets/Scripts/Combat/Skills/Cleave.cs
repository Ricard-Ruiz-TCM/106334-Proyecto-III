using UnityEngine;

[CreateAssetMenu(fileName = "Cleave", menuName = "Combat/Skills/Cleave")]
public class Cleave : Skill 
{

    public override void action(BasicActor from, Node to) {
        // TODO
        from.endAction();
    }

}
