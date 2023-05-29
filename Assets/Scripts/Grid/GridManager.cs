using System;
using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour {

    [SerializeField, Header("TAGS:")]
    private string _enemyTag = "Enemy";
    [SerializeField]
    private string _playerTag = "Player";

    /** Finding de alguien je, el m�s cercano */
    public Actor findByTag(Transform origin, string tag) {
        return (Actor)findSomeOne(origin, tag);
    }

    /** M�todo para buscar al player m�s cercano */
    public Actor findPlayer(Transform origin) {
        return (Actor)findSomeOne(origin, _playerTag);
    }

    /** M�todo para buscar al enemy m�s cercano */
    public Turnable findEnemy(Transform origin) {
        return (Actor)findSomeOne(origin, _enemyTag);
    }

    /** M�todo para buscar a alguien, interno */
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

    /** M�todo que busca y recupera un actor seg�n su nodo */
    public BasicActor getActor(Node node) {
        foreach (Turnable actor in TurnManager.instance.attenders) {
            if (Stage.StageBuilder.getGridNode(actor.transform.position) == node) {
                return (BasicActor)actor;
            }
        }
        return null;
    }

    /** M�todo que checkea si dos turnables est�n en rango, de casillas */
    public bool inRange(Turnable origin, Turnable target, int range) {
        GridPlane targetPlane = Stage.StageBuilder.getGridPlane(target.transform.position);
        GridPlane myPlane = Stage.StageBuilder.getGridPlane(origin.transform.position);
        return (Stage.StageBuilder.getDistance(myPlane, targetPlane) <= range || range == 0);
    }

    /** Busca una posicion inicial "random" */
    public GridPlane findPositionNode() {

        List<Node> innitials = new List<Node>();

        // Get de los nodos P
        Grid2D grid = Stage.Grid;
        for (int x = 0; x < grid.rows; x++) {
            for (int y = 0; y < grid.columns; y++) {
                if (grid.nodeMap[x, y].type.Equals(Array2DEditor.nodeType.P)) {
                    innitials.Add(grid.nodeMap[x,y]);
                }
            }
        }

        // Finde un Nodo P vacio
        for (int i = 0; i < innitials.Count; i++) {
            if (getActor(innitials[i]) == null) {
                return Stage.StageBuilder.getGridPlane(innitials[i]);
            }
        }

        // Tamos jodidos
        return null;
    }

}
