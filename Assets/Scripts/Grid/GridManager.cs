using System;
using UnityEngine;

public class GridManager : MonoBehaviour {

    [SerializeField, Header("TAGS:")]
    private string _enemyTag = "Enemy";
    [SerializeField]
    private string _playerTag = "Player";

    /** Método para buscar al player más cercano */
    public Actor findPlayer(Transform origin) {
        return (Actor)findSomeOne(origin, _playerTag);
    }

    /** Método para buscar al enemy más cercano */
    public Turnable findEnemy(Transform origin) {
        return (Actor)findSomeOne(origin, _enemyTag);
    }

    /** Método para buscar a alguien, interno */
    private Turnable findSomeOne(Transform origin, string tag) {
        Turnable nearest = null;
        float tmp = float.MaxValue;
        foreach (Turnable actor in TurnManager.instance.attenders) {
            // Check es player
            if (!actor.CompareTag(tag))
                continue;
            // Find cercano
            float dis = Vector3.Distance(origin.position, actor.transform.position);
            if (dis < tmp) {
                tmp = dis;
                nearest = actor;
            }
        }
        return nearest;
    }

    /** Método que checkea si dos turnables están en rango, de casillas */
    public bool inRange(Turnable origin, Turnable target, int range) {
        GridPlane targetPlane = Stage.StageBuilder.getGridPlane(target.transform.position);
        GridPlane myPlane = Stage.StageBuilder.getGridPlane(origin.transform.position);
        return (Stage.StageBuilder.getDistance(myPlane, targetPlane) <= range || range == 0);
    }

}
