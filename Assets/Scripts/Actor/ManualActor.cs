﻿using System.Collections.Generic;
using UnityEngine;

public class ManualActor : Actor {

    /** Nodoso de control para trabajar, temporales */
    private Node _mouseNode;
    private Node _myNode;

    List<Node> _walkablePath = new List<Node>();

    private skillID _tempSkillID;

    private bool _canMove = true;

    public void enableMovement() {
        _canMove = true;
    }

    public void disableMovement() {
        _canMove = false;
    }

    // Unity Start
    protected override void Start() {
        base.Start();
        transform.position = Stage.StageManager.findPositionNode().transform.position;
    }

    Actor anteriorEnemy;
    List<Node> anteriorArrowRain;

    /** Override del onTurn */
    //done, falta que no es vegi la grid quan no es pot moure
    public override void thinking() {

        // Solo Updateamos el path si hemos movido el ratón
        if (nodePositionChanged() && canMove() && canMoveIfBuff()) {
            Stage.StageBuilder.clearGrid();
            Stage.StageBuilder.DisplayMovementRange(transform, stepsRemain(), false);
            // Calcular la ruta nueva
            List<Node> route = Stage.Pathfinder.FindPath(_myNode, _mouseNode);
            if (route != null) {
                int steps = Mathf.Min(route.Count, stepsRemain());
                // Get de los paths
                _walkablePath = route.GetRange(0, steps);
            }
        }
        Actor enemy = Stage.StageBuilder.getMouseEnemyOrNPC();
        if (enemy != null) {
            displayInfoFrame();
        } else {
            anteriorEnemy = null;
            // Display de walkablePath
            if ((_walkablePath != null) && (_walkablePath.Count > 0)) {
                Stage.StageBuilder.displayPath(_walkablePath, pathMaterial.walkable, transform.position);
                // Si no estamos dentro del rango de movimineot, no aceptamos input
                Node target = Stage.StageBuilder.getMouseGridNode();
                if (!_walkablePath.Contains(target))
                    return;

                // Input
                if ((Input.GetMouseButtonDown(1)) && (canMove() && canMoveIfBuff())) //POSSIBLE BUG
                {
                    if (_canMove) {
                        setDestination(_walkablePath);
                        startMove();
                        _walkablePath = null;
                        // Animator
                        Anim.SetBool("moving", true);
                    }
                }
            }
        }



    }

    /** Override del en movmenet */
    public override void endMovement() {
        base.endMovement();
        Anim.SetBool("moving", false);
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
    BasicActor anteriorTarget;
    /** Override del act */
    public override void act() {

        // Reset de la acción si pulsamos escape
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Stage.StageBuilder.clearGrid();
            reAct();
        }

        transform.Rotate(new Vector3(10f, 0f, 0f));

        // Solo Updateamos el attack si hemos movido el ratón
        if (nodePositionChanged()) {
            Stage.StageBuilder.clearGrid();
            Stage.StageBuilder.displaySkillRange(equip.weapon.range, _myNode);
            // Check si estamos dentro de la distancia
            if (Stage.StageBuilder.getDistance(_mouseNode, _myNode) <= equip.weapon.range && Stage.StageBuilder.getGridPlane(_mouseNode).CanBeAttacked) {
                BasicActor target = Stage.StageManager.getActor(_mouseNode);

                Stage.StageBuilder.displaySkill(_tempSkillID, _mouseNode, pathMaterial.skill, this);

                if (target != null) {
                    if (target != anteriorTarget && anteriorTarget != null) {
                        if (anteriorTarget.entitieUI != null) {
                            anteriorTarget.entitieUI.GetComponent<EntitieUI>().hideSelectDamage();
                        }
                    }
                    anteriorTarget = target;
                    switch (_tempSkillID) {
                        default:
                            target.displayDamage(this, totalDamage(), equip.weapon.ID);
                            break;
                        case skillID.AchillesHeel:
                            target.displayDamage(this, (int)(totalDamage() * 1.4f), equip.weapon.ID);
                            break;
                        case skillID.DoubleLunge:
                            target.displayDamage(this, (totalDamage() * 2), equip.weapon.ID);
                            break;
                        case skillID.Cleave:
                            break;
                        case skillID.Disarm:
                            break;
                        case skillID.ArrowRain:
                            ArrowRainSkill(target);
                            break;
                    }

                } else 
                {
                    if(_tempSkillID == skillID.ArrowRain)
                    {                        
                        ArrowRainSkill(target);
                    }
                    if (anteriorTarget != null) {
                        if (anteriorTarget.entitieUI != null) {
                            if (anteriorTarget.entitieUI.GetComponent<EntitieUI>() != null) {
                                anteriorTarget.entitieUI.GetComponent<EntitieUI>().hideSelectDamage();
                            }
                        }

                    }
                }
            }
        }
        if (!Stage.StageBuilder.getGridPlane(_mouseNode).CanBeAttacked)
        {
            if (anteriorTarget != null)
            {
                if (anteriorTarget.entitieUI != null)
                {
                    anteriorTarget.entitieUI.GetComponent<EntitieUI>().hideSelectDamage();
                }
            }
            if (anteriorArrowRain != null)
            {
                foreach (var item in anteriorArrowRain)
                {
                    BasicActor targetArrow = Stage.StageManager.getActor(item);
                    if (targetArrow != null)
                    {
                        if (targetArrow.entitieUI != null)
                        {
                            ((Actor)targetArrow).entitieUI.GetComponent<EntitieUI>().hideSelectDamage();
                        }
                    }
                }
            }
        }
        // Chck si estamos en rango
        if (Stage.StageBuilder.getDistance(_mouseNode, _myNode) <= equip.weapon.range) 
        {
            // Input
            if (Input.GetMouseButtonDown(0) && Stage.StageBuilder.getGridPlane(_mouseNode).CanBeAttacked && _myNode != _mouseNode) {
                Stage.StageBuilder.clearGrid();
                BasicActor target = Stage.StageManager.getActor(_mouseNode);
                skills.useSkill(_tempSkillID, this, _mouseNode);
                UseSkill(_mouseNode);
            }
        }
        
        

    }
    private void ArrowRainSkill(BasicActor target)
    {
        if (anteriorArrowRain != null)
        {
            foreach (var item in anteriorArrowRain)
            {
                BasicActor targetArrow = Stage.StageManager.getActor(item);
                if (targetArrow != null)
                {
                    if (targetArrow.entitieUI != null)
                    {
                        ((Actor)targetArrow).entitieUI.GetComponent<EntitieUI>().hideSelectDamage();
                    }
                }
            }
        }

        List<Node> neight = Stage.Grid.getNeighbours(_mouseNode);
        anteriorArrowRain = neight;
        if (target != null)
        {
            target.displayDamage(this, (totalDamage()), equip.weapon.ID);
        }

        foreach (var item in neight)
        {

            BasicActor targetArrow = Stage.StageManager.getActor(item);
            if (targetArrow != null)
            {
                if (targetArrow.entitieUI != null)
                {
                    targetArrow.displayDamage(targetArrow, (totalDamage()), equip.weapon.ID);
                }
            }
        }
    }
    public override void beginTurn() {
        base.beginTurn();
        _canMove = true;
    }

    /*** Método para hacer el positioning del personaje */
    public void positioning() {
        nodePositionChanged();
        if (Input.GetMouseButtonDown(0) && _mouseNode != null) {
            if (_mouseNode.type.Equals(Array2DEditor.nodeType.P)) {
                BasicActor spot = Stage.StageManager.getActor(_mouseNode);
                if (spot != null) {
                    spot.transform.position = transform.position;
                }
                transform.position = Stage.StageBuilder.getGridPlane(_mouseNode).transform.position;
            }
        }
        displayInfoFrame();
    }

    public void displayInfoFrame() {
        Actor enemy = Stage.StageBuilder.getMouseEnemyOrNPC();
        if (enemy != null) {
            if (enemy != anteriorEnemy && !enemy.isDead) {
                Stage.StageBuilder.clearGrid();
                Stage.StageBuilder.DisplayMovementRange(enemy.transform, enemy.maxSteps(), true);
                Stage.StageBuilder.displaySkillRange(enemy.equip.weapon.range, Stage.StageBuilder.getGridNode(enemy.transform.position));

                Stage.StageBuilder.displayBothBordersActive();

                TargetInfoUI.instance.ShowPanel(enemy);

                anteriorEnemy = enemy;
            }
        } else {
            anteriorEnemy = null;
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

    public override void takeDamage(BasicActor from, int damage, itemID weapon = itemID.NONE) {
        base.takeDamage(from, damage, weapon);
        Anim.Play("TakeDamage");
    }

    public override void onActorDeath() {
        FMODManager.instance.PlayOneShot(FMODEvents.instance.PlayerDeath);
        Anim.SetTrigger("die");
        base.onActorDeath();
        //GameObject blood = Instantiate(Resources.Load("Particles/BloodDie") as GameObject);
        //Debug.Break();
        //blood.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
        //Destroy(blood, 2f);
    }

}
