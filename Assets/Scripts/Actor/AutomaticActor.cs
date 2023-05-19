using System.Collections.Generic;
using UnityEngine;

public class AutomaticActor : Actor {

    [SerializeField, Header("Inteligencia \"Artificial\":")]
    private combatAI _combatAI;

    [SerializeField, Range(0, 100), Header("Threshold para huir:")]
    private int _fleeThreshold = 20;

    /** Override del onTurn */
    public override void thinking() {
        switch (_combatAI) {
            case combatAI.ranged:
            case combatAI.melee:
                basicCombatAI();
                break;
            case combatAI.flee:
                fleeCombatAI();
                break;
            default:
                break;
        }
    }

    /** Método para establecer combate a distancia mínima */
    private void basicCombatAI() {
        if (healthPercent() < _fleeThreshold) {
            _combatAI = combatAI.flee;
            return;
        }
    }

    /** Método para indicar que toca moverse */
    private void makeTheMove() {

    }

    /** Método para indiar que toca actuar */
    private void makeTheAct() {

    }

    /** Método para establecer la IA de combate de huir */
    private void fleeCombatAI() {

        Turnable near = null;

        if (canAct())
            skills.useSkill(skillID.TrojanHorse, this);

        // Acting
        if (isMovementDone()) {
            if (canAct())
                skills.useSkill(skillID.TortoiseFormation, this);
            if (canAct())
                skills.useSkill(skillID.Defense, this);
            if (canAct()) {
                // Last Chance para atacar
                near = Stage.StageManager.findEnemy(transform);
                if (near != null) {
                    if (Stage.StageBuilder.getDistance(transform.position, near.transform.position) <= _equip.weapon.range){
                        Debug.Log("INTENTAMOS PEGAR CON ARMA, POR QUE ESTAMOS CLOSE");
                    }
                }
            }
            endAction();
        }


        // Moving
        if (!canMove())
            return;

        near = Stage.StageManager.findPlayer(transform);

        // Player encontrado
        if (near != null) {
            Node mPos = Stage.StageBuilder.getGridNode(transform.position);
            Node oPos = Stage.StageBuilder.getGridNode(near.transform.position);

            // Dirección
            int dirX = Mathf.Clamp(-(oPos.x - mPos.x), -1, 1);
            int dirY = Mathf.Clamp(-(oPos.y - mPos.y), -1, 1);

            // NuevaPos
            int newX = mPos.x + (dirX * stepsRemain());
            int newY = mPos.y + (dirY * stepsRemain());

            // Clamps
            newX = Mathf.Clamp(newX, 0, Stage.Grid.rows - 1);
            newY = Mathf.Clamp(newY, 0, Stage.Grid.columns - 1);

            // FindPath
            List<Node> path = Stage.Pathfinder.FindPath(mPos, Stage.StageBuilder.getGridNode(newX, newY));

            setDestination(path);
            startMove();
        } else {
            endMovement();
        }

    }

}
