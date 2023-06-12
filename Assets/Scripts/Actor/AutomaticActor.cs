using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticActor : Actor {

    [SerializeField, Header("Inteligencia \"Artificial\":")]
    private combatAI _combatAI;

    [SerializeField, Range(0, 100), Header("Threshold para huir:")]
    private int _fleeThreshold = 20;

    private bool _delayDone = false;
    [SerializeField, Header("Delay for acting/moving:")]
    private float _delayTime = 1.5f;

    /** Override del onTurn */
    public override void thinking() {

        if (!_delayDone) {
            StartCoroutine(CThinking());
        }
    }

    private IEnumerator CThinking() {
        _delayDone = true;
        yield return new WaitForSeconds(_delayTime);
        // Change to flee if low health
        if ((!_combatAI.Equals(combatAI.flee)) && (healthPercent() <= _fleeThreshold)) {
            _combatAI = combatAI.flee;
        }

        // AI de combate
        switch (_combatAI) {
            case combatAI.ranged:
            case combatAI.melee:
                mixedCombatAI();
                break;
            case combatAI.flee:
                fleeCombatAI();
                break;
            default:
                endAction();
                endMovement();
                break;
        }
        _delayDone = false;
    }


    #region combatAI.ranged && combatAI.melee
    /** Método para establecer combate a distancia mínima */
    private void mixedCombatAI() {
        // Movimiento
        if (canMove()) {
            if (canMoveIfBuff()) {
                moveMinWeaponRangeDistance();
            } else {
                endMovement();
            }
        }

        // Acting
        if (isMovementDone() && canAct()) {
            if (canActIfBuff()) {
                attackPriositisingSkills();
            } else {
                endAction();
            }
        }

    }
    #endregion

    #region combatAI.flee
    /** Método para establecer la IA de combate de huir */
    private void fleeCombatAI() {
        // Acting
        if (canAct()) {
            if (canActIfBuff()) {
                attackPriositingMovementNDefensiveSkills();
            } else {
                endAction();
            }
        }

        // Moving
        if (canMove()) {
            if (canMoveIfBuff()) {
                moveFarAway();
            } else {
                endMovement();
            }
        }
    }
    #endregion

    #region combatAI's
    /** Métodos de Acting */
    private void attackPriositingMovementNDefensiveSkills() {
        Turnable near;

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
                near = Stage.StageManager.findByTag(transform, "Player");
                if (near != null) {
                    if (Stage.StageBuilder.getDistance(transform.position, near.transform.position) <= _equip.weapon.range) {
                        skills.useSkill(skillID.Attack, this, Stage.StageBuilder.getGridNode(near.transform.position));
                        UseSkill(Stage.StageBuilder.getGridNode(near.transform.position));
                        // Anim
                        if (equip.weapon.ID.Equals(itemID.Bow)) {
                            Anim.SetTrigger("attackBow");
                        } else {
                            Anim.SetTrigger("attackWeapon");
                        }
                    }
                }
            }
            endAction();
        }
    }
    private void attackPriositisingSkills() {

        Turnable near = Stage.StageManager.findByTag(transform, "Player");

        // Player Encontrado
        if (near != null) {

            bool inWeaponRange = (Stage.StageBuilder.getDistance(transform.position, near.transform.position) <= _equip.weapon.range);

            // Skills + end with combat
            if (canAct())
                skills.useSkill(skillID.ImperialCry, this);
            if (canAct() && inWeaponRange && CanAttack())
                skills.useSkill(skillID.Bloodlust, this, Stage.StageBuilder.getGridNode(near.transform.position));
            if (canAct() && inWeaponRange)
                skills.useSkill(skillID.Disarm, this, Stage.StageBuilder.getGridNode(near.transform.position));
            if (canAct() && inWeaponRange)
                skills.useSkill(skillID.AchillesHeel, this, Stage.StageBuilder.getGridNode(near.transform.position));
            if (canAct() && inWeaponRange && CanAttack())
                skills.useSkill(equip.weapon.skill, this, Stage.StageBuilder.getGridNode(near.transform.position));
            if (canAct()) {
                if (inWeaponRange && CanAttack()) {
                    skills.useSkill(skillID.Attack, this, Stage.StageBuilder.getGridNode(near.transform.position));
                    // Anim
                    if (equip.weapon.ID.Equals(itemID.Bow)) {
                        Anim.SetTrigger("attackBow");
                    } else {
                        Anim.SetTrigger("attackWeapon");
                    }
                } else {
                    // Si no, usamos vanish
                    if (canAct())
                        skills.useSkill(skillID.Vanish, this);
                }
            }
        }

        endAction();
    }
    private void attackIfImAlone() {
    }

    /** Métodos de Moving */
    private void moveFarAway() {
        Turnable near = Stage.StageManager.findByTag(transform, "Player");

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
    private void moveMinWeaponRangeDistance() {
        Turnable near = Stage.StageManager.findByTag(transform, "Player");

        // Player encontrado
        if (near != null) {
            Node mPos = Stage.StageBuilder.getGridNode(transform.position);
            Node oPos = Stage.StageBuilder.getGridNode(near.transform.position);

            Node destiny = Stage.StageBuilder.findClosestNode(mPos, oPos);

            // FindPath
            List<Node> path = Stage.Pathfinder.FindPath(mPos, destiny);
            // Salimos si no hay path
            if (path == null) {
                endMovement();
                return;
            }

            // Cortamos el path aa la distancia mínima del arma
            if (path.Count > equip.weapon.range + 1) {
                path.Reverse();
                path.RemoveRange(0, equip.weapon.range);
                path.Reverse();
            } else {
                path.Clear();
            }

            // Cortamos el path al movimiento mínimo
            if (stepsRemain() < path.Count) {
                int steps = Mathf.Min(path.Count, stepsRemain());
                path.RemoveRange(steps, path.Count - steps);
            }

            setDestination(path);
            startMove();
        } else {
            endMovement();
        }
    }
    private void moveCloseToAllies() {
    }
    #endregion


    public override void startMove() {
        base.startMove();
        Anim.SetBool("moving", true);
    }

    /** Override del en movmenet */
    public override void endMovement() {
        base.endMovement();
        Anim.SetBool("moving", false);
    }

    public override void takeDamage(BasicActor from, int damage, itemID weapon = itemID.NONE) {
        base.takeDamage(from, damage, weapon);
        Anim.SetTrigger("takeDamage");
    }

    public override void onActorDeath() {
        base.onActorDeath();
        FMODManager.instance.PlayOneShot(FMODEvents.instance.SoldierDeath);
        FMODManager.instance.PlayOneShot(FMODEvents.instance.Disarm);
        Anim.SetTrigger("die");
        //GameObject blood = Instantiate(Resources.Load("Particles/BloodDie") as GameObject);
        //Debug.Break();
        //blood.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
        //Destroy(blood, 2f);
    }

}
