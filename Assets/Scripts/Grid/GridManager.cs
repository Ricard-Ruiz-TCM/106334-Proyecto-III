using UnityEngine;

public class GridManager : MonoBehaviour {

    public Turnable FindNearestPlayer(Transform me)
    {

        Turnable actor = null;
        float dist = Mathf.Infinity;

        foreach (Turnable obj in CombatManager.instance.FindPlayers())
        {
            float distance = Vector3.Distance(obj.transform.position, me.position);
            if (distance < dist)
            {
                if (!obj.Status.isStatusActive(buffsID.Invisible))
                {
                    actor = obj;
                    dist = distance;
                }
            }
        }

        return actor;
    }
    public Turnable FindNearestEnemy(Transform me)
    {

        Turnable actor = null;
        float dist = Mathf.Infinity;

        foreach (Turnable obj in CombatManager.instance.FindEnemys())
        {
            float distance = Vector3.Distance(obj.transform.position, me.position);
            if (distance < dist)
            {
                if (!obj.Status.isStatusActive(buffsID.Invisible))
                {
                    actor = obj;
                    dist = distance;
                }
            }
        }

        return actor;
    }


    public bool InRange(Transform me, Turnable target, int range) {
        GridPlane targetPlane = Stage.StageBuilder.GetGridPlane(target.transform.position);
        GridPlane myPlane = Stage.StageBuilder.GetGridPlane(me.position);
        return (Stage.StageBuilder.GetGridDistanceBetween(myPlane, targetPlane) <= range || range == 0);
    }

    public Vector3 RandomInnitialPosition() {
        Vector3 pos = Vector3.zero;
        do {
            for (int x = 0; x < Stage.StageGrid.Columns; x++) {
                for (int y = 0; y < Stage.StageGrid.Rows; y++) {
                    if (Stage.StageGrid.isType(x, y, Array2DEditor.nodeType.P)) {
                        return Stage.StageBuilder.GetGridPlane(x, y).transform.position;
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
