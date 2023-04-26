using UnityEngine;

[CreateAssetMenu(fileName = "new Attack", menuName = "Combat/Skills/Attack")]
public class Attack : Skill 
{

    public override void Special(Actor from) 
    {
        FindObjectOfType<CombatManager>().Prova(from,_range);
        Debug.Log(from.Damage());
    }

}
