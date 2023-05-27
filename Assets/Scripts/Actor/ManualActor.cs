using System;
using System.Collections.Generic;
using UnityEngine;

public class ManualActor : Actor {

    public static event Action onSkillUsed;

    [SerializeField]
    private Node _mouseNode;
    [SerializeField]
    private Node _myNode;

    List<Node> _walkablePath = new List<Node>();

    private skillID _tempSkillID;

    /** Override del onTurn */
    //done, falta que no es vegi la grid quan no es pot moure
    public override void thinking() {

        // Solo Updateamos el path si hemos movido el ratón
        if (nodePositionChanged() && canMove()) {
            Stage.StageBuilder.clearGrid();
            Stage.StageBuilder.DisplayMovementRange(transform, stepsRemain());
            // Calcular la ruta nueva
            List<Node> route = Stage.Pathfinder.FindPath(_myNode, _mouseNode);
            if (route != null) {
                int steps = Mathf.Min(route.Count, stepsRemain());
                // Get de los paths
                _walkablePath = route.GetRange(0, steps);
            }
        }

        // Display de walkablePath
        if ((_walkablePath != null) && (_walkablePath.Count > 0)) {
            Stage.StageBuilder.displayPath(_walkablePath, pathMaterial.walkable);
            // Si no estamos dentro del rango de movimineot, no aceptamos input
            Node target = Stage.StageBuilder.getMouseGridNode();
            if (!_walkablePath.Contains(target))
                return;

            // Input
            if ((Input.GetMouseButtonDown(0)) && (canMove())) {
                setDestination(_walkablePath);
                startMove();
                _walkablePath = null;
            }
        }

    }

    /** Override del can move pra cuando tenemos movimientos pendientes todavía */
    public override bool canMove() {
        return (base.canMove() && (stepsRemain() > 0));
    }

    /** Set de las skill que queremos utilizar para empezar a seleccionar target */
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
            Stage.StageBuilder.clearGrid();
            reAct();
        }

        // Solo Updateamos el attack si hemos movido el ratón
        if (nodePositionChanged()) {
            Stage.StageBuilder.clearGrid();
            Stage.StageBuilder.displaySkillRange(equip.weapon.range, _myNode);
            // Check si estamos dentro de la distancia
            if (Stage.StageBuilder.getDistance(_mouseNode, _myNode) <= equip.weapon.range) {
                Stage.StageBuilder.displaySkill(_tempSkillID, _mouseNode, pathMaterial.skill);
            }
        }

        // Chck si estamos en rango
        if (Stage.StageBuilder.getDistance(_mouseNode, _myNode) <= equip.weapon.range) {
            // Input
            if (Input.GetMouseButtonDown(0)) {
                Stage.StageBuilder.clearGrid();
                BasicActor target = Stage.StageManager.getActor(_mouseNode);
                skills.useSkill(_tempSkillID, this, target);
                onSkillUsed?.Invoke();
            }
        }

    }

    /** Override del reAct */
    public override void reAct() {
        _tempSkillID = skillID.NONE;
        base.reAct();
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
