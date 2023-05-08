using UnityEngine;

[CreateAssetMenu(fileName = "new Vanish", menuName = "Combat/Skills/Vanish")]
public class Vanish : Skill {

    public override void Special(Actor from) {
        from.GetComponent<ActorStatus>().ApplyStatus(aStatus.Invisible);
        from.EndAction();
        Debug.Log("Vanish special attack");
    }

}
