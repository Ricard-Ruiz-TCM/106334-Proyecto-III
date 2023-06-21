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

        if (canAct())
            skills.useSkill(skillID.TrojanHorse, this);
        if (canAct())
            skills.useSkill(skillID.Vanish, this);

        // Acting
        if (isMovementDone()) {
            if (canAct())
                skills.useSkill(skillID.TortoiseFormation, this);
            if (canAct())
                skills.useSkill(skillID.Defense, this);
            if (canAct()) 
            {
                List<Turnable> alonso = getAllPlayers();
                // Player Encontrado
                if (alonso.Count != 0)
                {
                    List<Turnable> personsInRange = playerInRange(alonso,transform.position);

                    if (personsInRange.Count != 0)
                    {

                        Turnable lowestPerson = getLowestHpPlayer(personsInRange);
                        if (canAct())
                        {
                            skills.useSkill(skillID.Bloodlust, this, Stage.StageBuilder.getGridNode(lowestPerson.transform.position));
                        }
                        if (canAct())
                        {
                            skills.useSkill(skillID.Attack, this, Stage.StageBuilder.getGridNode(lowestPerson.transform.position));
                        }


                        UseSkill(Stage.StageBuilder.getGridNode(lowestPerson.transform.position));
                        // Anim
                        if (equip.weapon.ID.Equals(itemID.Bow))
                        {
                            Anim.Play("Bow");
                        }
                        else
                        {
                            Anim.Play("Attack" + Random.Range(0, 2).ToString());
                        }
                    }
                }
            }
            endAction();
        }
    }
    private List<Turnable> getAllPlayers()
    {
        List<Turnable> alonso = new List<Turnable>(TurnManager.instance.attenders.ToArray());
        alonso.RemoveAll(x => x is AutomaticActor);
        alonso.RemoveAll(x => x is StaticActor);
        return alonso;
    }
    private List<Turnable> playerInRange(List<Turnable> alonso, Vector3 pos)
    {
        List<Turnable> personsInRange = new List<Turnable>();
        List<Node> rangeListNodes = Stage.StageBuilder.rangeList(equip.weapon.range, Stage.StageBuilder.getGridNode(pos));

        foreach (Turnable persona in alonso)
        {
            Node nodePersona = Stage.StageBuilder.getGridNode(persona.transform.position);
            foreach (Node item in rangeListNodes)
            {
                if (item == nodePersona)
                {
                    personsInRange.Add(persona);
                    break;
                }
            }
        }
        return personsInRange;
    }
    private Turnable getLowestHpPlayer(List<Turnable> personsInRange)
    {
        Turnable lowestPerson = null;
        foreach (Turnable item in personsInRange)
        {
            if (lowestPerson == null)
            {
                lowestPerson = item;
            }
            else
            {
                if (((Actor)item).health() < ((Actor)lowestPerson).health())
                {
                    lowestPerson = item;
                }
            }
        }
        return lowestPerson;
    }
    private void attackPriositisingSkills() {

        List<Turnable> alonso = getAllPlayers();
        // Player Encontrado
        if (alonso.Count != 0)
        {
            List<Turnable> personsInRange = playerInRange(alonso,transform.position);

            if (personsInRange.Count != 0)
            {

                Turnable lowestPerson = getLowestHpPlayer(personsInRange);

                //bool inWeaponRange = (Stage.StageBuilder.getDistance(transform.position, lowestPerson.transform.position) <= _equip.weapon.range); posible bug oir quitarlo en el if de can act

                // Skills + end with combat
                if (canAct())
                skills.useSkill(skillID.ImperialCry, this);
            if (canAct()  && CanAttack())
                skills.useSkill(skillID.Bloodlust, this, Stage.StageBuilder.getGridNode(lowestPerson.transform.position));
            if (canAct())
                skills.useSkill(skillID.Disarm, this, Stage.StageBuilder.getGridNode(lowestPerson.transform.position));
            if (canAct())
                skills.useSkill(skillID.AchillesHeel, this, Stage.StageBuilder.getGridNode(lowestPerson.transform.position));
            if (canAct() && CanAttack())
                skills.useSkill(equip.weapon.skill, this, Stage.StageBuilder.getGridNode(lowestPerson.transform.position));
            if (canAct())
            {
                if ( CanAttack())
                {
                    skills.useSkill(skillID.Attack, this, Stage.StageBuilder.getGridNode(lowestPerson.transform.position));
                }
                else
                {
                    // Si no, usamos vanish
                    if (canAct())
                        skills.useSkill(skillID.Vanish, this);
                }
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

            // Cortamos el path al movimiento mínimo
            if (stepsRemain() < path.Count) {
                int steps = Mathf.Min(path.Count, stepsRemain());
                path.RemoveRange(steps, path.Count - steps);
            }


            List<Node> defenitivePath = new List<Node>(path.ToArray());
            defenitivePath.Insert(0,Stage.StageBuilder.getGridNode(transform.position));
            path.Insert(0, Stage.StageBuilder.getGridNode(transform.position));
            for (int i = 0; i < path.Count; i++)
            {

                List<Turnable> alonso = getAllPlayers();

                List<Turnable> personsInRange = playerInRange(alonso, Stage.StageBuilder.getGridPlane(path[i]).position);

                if (personsInRange.Count != 0)
                {
                    for (int z = 0; z < path.Count - i - 1; z++)
                    {
                        defenitivePath.Reverse();
                        defenitivePath.RemoveAt(0);
                        defenitivePath.Reverse();
                    }
                    break;
                }
                //else
                //{
                //    path.Clear();
                //}
            }
            defenitivePath.RemoveAt(0);
            path.RemoveAt(0);

            setDestination(defenitivePath);
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
        Anim.Play("TakeDamage");
    }

    public override void onActorDeath() {
        FMODManager.instance.PlayOneShot(FMODEvents.instance.SoldierDeath);
        GetComponent<WeaponHolder>().throwWeapon();
        FMODManager.instance.PlayOneShot(FMODEvents.instance.Disarm);
        Anim.SetTrigger("die");
        base.onActorDeath();
        //GameObject blood = Instantiate(Resources.Load("Particles/BloodDie") as GameObject);
        //Debug.Break();
        //blood.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
        //Destroy(blood, 2f);
    }

}
