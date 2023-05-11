using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "new MoralizingShout", menuName = "Combat/Skills/Moralizing Shout")]
public class MoralizingShout : Skill 
{
    [SerializeField] SkinnedMeshRenderer skinnedMesh;
    [SerializeField] Material shaderMat;
    public override void Special(Actor from) 
    {
        skinnedMesh.materials[1] = shaderMat;
        CombatManager.instance.UseSkill(from, range, skill, from.canInteract);
        from.EndAction();
        Debug.Log("MoralizingShout special attack");
    }

}
