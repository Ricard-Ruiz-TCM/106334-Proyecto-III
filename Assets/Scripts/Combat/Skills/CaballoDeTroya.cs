using UnityEngine;
[CreateAssetMenu(fileName = "new Troya", menuName = "Combat/Skills/CaballoDeTroya")]
public class CaballoDeTroya : Skill {
    public override void Special(Actor from) {
        from.IsInvisible();
    }
}
