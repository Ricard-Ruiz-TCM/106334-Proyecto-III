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



        // Moving
        if (canMove()) {

            Turnable near = Stage.StageManager.findPlayer(transform);

            // Player encontrado
            if (near != null) {
                Node mPos = Stage.StageBuilder.getGridNode(transform.position);
                Node oPos = Stage.StageBuilder.getGridNode(near.transform.position);

                Node destiny = Stage.StageBuilder.findClosestNode(mPos, oPos);

                // FindPath
                List<Node> path = Stage.Pathfinder.FindPath(mPos, destiny);

                // Cortamos el path aa la distancia mínima del arma
                if ((path != null) && (path.Count > equip.weapon.range)) {
                    path.Reverse();
                    path.RemoveRange(0, equip.weapon.range);
                    path.Reverse();
                }

                // Cortamos el path al movimiento mínimo
                if ((path != null) && (stepsRemain() > 0)) {
                    int steps = Mathf.Min(path.Count, stepsRemain());
                    path.RemoveRange(steps, path.Count - steps);
                }

                setDestination(path);
                startMove();
            } else {
                endMovement();
            }
        }

        // Acting
        if (canAct()) {

            // Solo actuamos si ya nos movimos
            if (!isMovementDone())
                return;

            Turnable near = Stage.StageManager.findPlayer(transform);

            // Player Encontrado
            if (near != null) {

                bool inWeaponRange = (Stage.StageBuilder.getDistance(transform.position, near.transform.position) <= _equip.weapon.range);

                // Skills + end with combat
                if (canAct())
                    skills.useSkill(skillID.ImperialCry, this);
                if (canAct() && inWeaponRange)
                    skills.useSkill(skillID.Bloodlust, this, (BasicActor)near);
                if (canAct() && inWeaponRange)
                    skills.useSkill(skillID.Disarm, this, (BasicActor)near);
                if (canAct() && inWeaponRange)
                    skills.useSkill(skillID.AchillesHeel, this, (BasicActor)near);
                if (canAct() && inWeaponRange)
                    skills.useSkill(equip.weapon.skill, this, (BasicActor)near);
                if (canAct()) {
                    // Last Chance para atacar
                    near = Stage.StageManager.findEnemy(transform);
                    if (near != null) {
                        if (inWeaponRange) {
                            skills.useSkill(skillID.Attack, this, (BasicActor)near);
                        } else {
                            // Si no, usamos vanish
                            if (canAct())
                                skills.useSkill(skillID.Vanish, this);
                        }
                    }
                }
            }
            endAction();
        }

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
                    if (Stage.StageBuilder.getDistance(transform.position, near.transform.position) <= _equip.weapon.range) {
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
