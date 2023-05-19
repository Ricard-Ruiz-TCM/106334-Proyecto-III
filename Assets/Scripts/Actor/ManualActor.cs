using System.Collections.Generic;
using UnityEngine;

public class ManualActor : Actor {

    [SerializeField]
    private Node _mouseNode;
    [SerializeField]
    private Node _myNode;

    List<Node> _extraPath = new List<Node>();
    List<Node> _walkablePath = new List<Node>();

    private skillID _tempSkillID;

    /** Override del onTurn */
    public override void thinking() {

        if (nodePositionChanged()) {
            Stage.StageBuilder.clearGrid();

            // Calcular la ruta nueva
            List<Node> route = Stage.Pathfinder.FindPath(_myNode, _mouseNode);
            if ((route != null) && (stepsRemain() > 0)) {
                int steps = Mathf.Min(route.Count, stepsRemain());
                // Get de los paths
                _walkablePath = route.GetRange(0, steps);
                _extraPath = route.GetRange(steps, Mathf.Clamp(route.Count - steps, 0, route.Count));
            }
        }

        // Display de los paths
        if (_walkablePath.Count > 0) {
            Stage.StageBuilder.displayPath(_walkablePath, pathMaterial.walkable);
        }
        // Display de los paths
        if (_extraPath.Count > 0) {
            Stage.StageBuilder.displayPath(_extraPath, pathMaterial.notWalkable);
        }

        // Input
        if (Input.GetMouseButtonDown(0) && _walkablePath.Count > 0) {
            if (canMove()) {
                setDestination(_walkablePath);
                startMove();
            }
        }

    }

    /** Override del can move pra cuando tenemos movimientos pendientes todavía */
    public override bool canMove() {
        return (stepsRemain() > 0);
    }

    /** Selección de target?? */
    public void setSkillToUse(skillID id) {
        if (id.Equals(skillID.NONE) || !skills.haveSkill(id) || !skills.canUse(id)) {
            reAct();
            return;
        }

        _tempSkillID = id;
    }

    /** Override del act */
    public override void act() {
        // Reset de la acción si pulsamos escape
        if (Input.GetKeyDown(KeyCode.Escape)) {
            reAct();
        }

        if (nodePositionChanged()) {
            Stage.StageBuilder.clearGrid();
            // Check si estamos dentro de la distancia
            if (Stage.StageBuilder.getDistance(_mouseNode, _myNode) <= equip.weapon.range) {
                Stage.StageBuilder.displaySkill(_tempSkillID, _mouseNode, pathMaterial.skill);
            } else {
                Stage.StageBuilder.displaySkill(_tempSkillID, _mouseNode, pathMaterial.NONE);
            }
        }

        // Chck si estamos en rango
        if (Stage.StageBuilder.getDistance(_mouseNode, _myNode) <= equip.weapon.range) {
            if (Input.GetMouseButtonDown(0)) {
                BasicActor target = Stage.StageManager.getActor(_mouseNode);
                skills.useSkill(_tempSkillID, this, target);
            }
        }

    }

    /** Override del reAct */
    public override void reAct() {
        base.reAct();
        _tempSkillID = skillID.NONE;
        Stage.StageBuilder.clearGrid();
    }

    /** Método que actualiza la posicón del ratón y mi posición en nodos, devuelve true si ha habido cambios */
    private bool nodePositionChanged() {
        // Get del node del ratón
        Node mNode = Stage.StageBuilder.getMouseGridNode();
        if ((mNode != null) && (mNode != _mouseNode)) {
            _mouseNode = mNode;
            _myNode = Stage.StageBuilder.getGridNode(transform.position);
            return true;
        }
        return false;
    }

}
