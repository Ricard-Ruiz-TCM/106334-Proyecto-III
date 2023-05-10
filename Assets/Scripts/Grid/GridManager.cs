using UnityEngine;

public class GridManager : MonoBehaviour {

    public Actor FindNearestPlayers(Transform me) {

        Actor actor = null;
        float dist = Mathf.Infinity;

        foreach (Actor obj in CombatManager.instance.FindPlayers()) {
            float distance = Vector3.Distance(obj.transform.position, me.position);
            if (distance < dist) {
                if (obj.Status.isStatusActive(buffsnDebuffs.Invisible)) {
                    actor = obj;
                    dist = distance;
                }
            }
        }

        return actor;
    }

    public bool InRange(Transform me, Actor target, int range) {
        GridPlane targetPlane = Stage.StageBuilder.GetGridPlane(target.transform.position);
        GridPlane myPlane = Stage.StageBuilder.GetGridPlane(me.position);
        return Stage.StageBuilder.GetGridDistanceBetween(myPlane, targetPlane) <= range;
    }

}
