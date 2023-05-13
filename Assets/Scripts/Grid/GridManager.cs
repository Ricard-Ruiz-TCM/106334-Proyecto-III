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

    public Vector3 RandomInnitialPosition() {
        Vector3 pos = Vector3.zero;
        do {
            for (int x = 0; x < Stage.StageGrid.Columns; x++) {
                for (int y = 0; y < Stage.StageGrid.Rows; y++) {
                    if (Stage.StageGrid.isType(x, y, Array2DEditor.nodeType.P)) {
                        return Stage.StageBuilder.GetGridPlane(x, y).position;
                    }
                }
            }
        } while (pos == Vector3.zero);
        return pos;
    }

    public Vector3 RandomPosition() {
        return Stage.StageBuilder.GetGridPlane(Random.Range(0, Stage.StageGrid.Columns - 1), Random.Range(0, Stage.StageGrid.Rows - 1)).position;
    }

}
