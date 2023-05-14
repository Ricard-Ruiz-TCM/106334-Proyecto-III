using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : ActorsListController {

    public static CombatManager instance = null;

    [SerializeField] LayerMask layerActor;
    [SerializeField] Material shootMat;

    // Unity Awake
    private void Awake() {
        // Singleton
        if ((instance != null) && (instance != this)) {
            GameObject.Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    public List<Actor> FindPlayers()
    {
        return _actors.FindAll(x => x is Player);
    }
    public List<Actor> FindEnemys()
    {
        return _actors.FindAll(x => x is Enemy);
    }

    //metodos para hacer las skills
    //los que no estan aqui se hacen desde su scriptableObject, eso es porque la skill se hace sin tener que seleccionar ninguna casilla

    public void UseSkill(Actor actor, int range, skills skillType, bool canInteract) {
        actor.canMove = false;

        switch (skillType) {
            case skills.Attack:
                StartCoroutine(NormalAttack(actor, range, skillType, canInteract));
                break;
            case skills.DoubleLunge:
                StartCoroutine(NormalAttack(actor, range, skillType, canInteract));
                break;
            case skills.Cleave:
                StartCoroutine(ShowGolpeDemoledor(actor, range, skillType, canInteract));
                break;
            case skills.ArrowRain:
                StartCoroutine(ShowArrows(actor, range, skillType, canInteract));
                break;
            case skills.MoralizingShout:
                StartCoroutine(ShowMoralizingShout(actor, range, skillType));
                break;
            case skills.Disarm:
                StartCoroutine(NormalAttack(actor, range, skillType, canInteract));
                break;
            case skills.SedDeSangre:
                StartCoroutine(NormalAttack(actor, range, skillType, canInteract));
                break;
        }

    }

    //enumerators que funcionan como "updates" para hacer las funciones de las skills
    IEnumerator ShowGolpeDemoledor(Actor actor, int range, skills skillType, bool canInteract) {
        bool canEnd = false;
        Node node = null;

        if (canInteract) {
            while (!canEnd) {

                GetPosToAttack(actor, range, out node, node);

                if (uCore.Action.GetKeyDown(KeyCode.J)) {
                    canEnd = true;
                    GetActorInNode(node, actor, skillType);

                }
                if (node != null) {
                    ExtendGolpeDemoledor(actor, range, skillType, node, canEnd);
                }
                if (uCore.Action.GetKeyDown(KeyCode.Escape)) {
                    canEnd = true;
                }
                yield return null;
            }
        } else 
        {
            GetPosToEnemyAttack(actor, out node);
            if (node != null) {
                GetActorInNode(node, actor, skillType);
                ExtendGolpeDemoledor(actor, range, skillType, node, false);
            }
        }

        StartCoroutine(EndAttack(actor));
    }
    //public Actor FindNearestPlayer(Actor from) {

    //    Actor actor = null;
    //    float dist = Mathf.Infinity;

    //    foreach (Actor obj in FindPlayers()) {
    //        float distance = Vector3.Distance(obj.transform.position, from.transform.position);
    //        if (distance < dist) {
    //            if (!obj.Status.isStatusActive(buffsnDebuffs.Invisible)) {
    //                actor = obj;
    //                dist = distance;
    //            }
    //        }
    //    }

    //    return actor;
    //}
    //public Actor FindNearestEnemy(Actor from)
    //{

    //    Actor actor = null;
    //    float dist = Mathf.Infinity;

    //    foreach (Actor obj in FindEnemys())
    //    {
    //        float distance = Vector3.Distance(obj.transform.position, from.transform.position);
    //        if (distance < dist)
    //        {
    //            if (!obj.Status.isStatusActive(buffsnDebuffs.Invisible))
    //            {
    //                actor = obj;
    //                dist = distance;
    //            }
    //        }
    //    }

    //    return actor;
    //}
    private void ExtendGolpeDemoledor(Actor actor, int range, skills skillType, Node node, bool canEnd) {
        if (Mathf.RoundToInt(actor.transform.position.x / 10) == node.x) {
            ExtendAttack(node.x + 1, node.y, actor, canEnd, skillType);

            ExtendAttack(node.x - 1, node.y, actor, canEnd, skillType);

            if (Mathf.RoundToInt(actor.transform.position.z / 10) > node.y) {
                ExtendAttack(node.x, node.y - 1, actor, canEnd, skillType);
            } else {
                ExtendAttack(node.x, node.y + 1, actor, canEnd, skillType);
            }
        }



        if (Mathf.RoundToInt(actor.transform.position.z / 10) == node.y) {
            ExtendAttack(node.x, node.y + 1, actor, canEnd, skillType);

            ExtendAttack(node.x, node.y - 1, actor, canEnd, skillType);

            if (Mathf.RoundToInt(actor.transform.position.x / 10) > node.x) {
                ExtendAttack(node.x - 1, node.y, actor, canEnd, skillType);
            } else {
                ExtendAttack(node.x + 1, node.y, actor, canEnd, skillType);
            }


        }
    }

    IEnumerator NormalAttack(Actor actor, int range, skills skillType, bool canInteract) {
        bool canEnd = false;
        Node node = null;

        if (canInteract) {
            while (!canEnd) {

                GetPosToAttack(actor, range, out node, node);

                if (uCore.Action.GetKeyDown(KeyCode.J)) {
                    canEnd = true;
                    GetActorInNode(node, actor, skillType);

                }
                if (uCore.Action.GetKeyDown(KeyCode.Escape)) {
                    canEnd = true;
                }
                yield return null;
            }
        } else 
        {
            GetPosToEnemyAttack(actor, out node);
            if (node != null) {
                GetActorInNode(node, actor, skillType);
            }
        }
        StartCoroutine(EndAttack(actor));

    }

    IEnumerator ShowArrows(Actor actor, int range, skills skillType, bool canInteract) {
        bool canEnd = false;
        Node node = null;
        //List<Node> nodes = new List<Node>();

        if (canInteract) {
            while (!canEnd) {

                Stage.StageBuilder.ClearGrid();

                GetPosToAttack(actor, range, out node,node);

                if (uCore.Action.GetKeyDown(KeyCode.J)) {
                    GetActorInNode(node, actor, skillType);
                    canEnd = true;
                }
                if (node != null) {
                    ExtendAttack(node.x + 1, node.y, actor, canEnd, skillType);

                    ExtendAttack(node.x - 1, node.y, actor, canEnd, skillType);

                    ExtendAttack(node.x, node.y + 1, actor, canEnd, skillType);

                    ExtendAttack(node.x, node.y - 1, actor, canEnd, skillType);

                }
                if (uCore.Action.GetKeyDown(KeyCode.Escape)) {
                    canEnd = true;
                }
                yield return null;
            }
        } 
        else 
        {
            GetPosToEnemyAttack(actor, out node);
            if (node != null) {
                GetActorInNode(node, actor, skillType);
                ExtendAttack(node.x + 1, node.y, actor, canEnd, skillType);

                ExtendAttack(node.x - 1, node.y, actor, canEnd, skillType);

                ExtendAttack(node.x, node.y + 1, actor, canEnd, skillType);

                ExtendAttack(node.x, node.y - 1, actor, canEnd, skillType);
            }
        }
        StartCoroutine(EndAttack(actor));

    }
    IEnumerator ShowMoralizingShout(Actor actor, int range, skills skillType) {
        Node node = null;
        //List<Node> nodes = new List<Node>();


        node = Stage.StageBuilder.GetGridPlane(Mathf.RoundToInt(actor.transform.position.x / 10), Mathf.RoundToInt(actor.transform.position.z / 10)).node;

        int count = 1;
        int totalCount = 1;
        for (int i = node.x - range; i <= node.x + range; i++) {
            for (int j = node.y; j < count + node.y; j++) {
                ExtendAttack(i, j, actor, true, skillType);
            }
            for (int z = node.y - 1; z > node.y - count; z--) {
                ExtendAttack(i, z, actor, true, skillType);
            }

            if (totalCount > range) {
                count--;
            } else {
                count++;
            }
            totalCount++;
        }
        yield return null;
        StartCoroutine(EndAttack(actor));
    }
    IEnumerator EndAttack(Actor actor) {
        yield return new WaitForSeconds(1f);
        Stage.StageBuilder.ClearGrid();
        actor.canMove = true;
    }

    //metodo para los ataques que tienen da�o en area (comprueba si esta dentro de la grid - cambia el material - y si hay un enemigo hace efecto)
    private void ExtendAttack(int i, int j, Actor actor, bool canEnd, skills skillType) {
        if (ChechIfPositionIsInGrid(i, j)) {
            Stage.StageBuilder.UpdateMaterial(i, j, shootMat);
            if (canEnd) 
            {
                GetActorInNode(Stage.StageBuilder.GetGridPlane(i, j).node, actor, skillType);
            }

        }
    }

    //metodo para checkear si hay un actor en un nodo y aplica la skill en el actor
    private void GetActorInNode(Node node, Actor from, skills skillType) {
        for (int i = 0; i < _actors.Count; i++) {
            if (node.x == Mathf.RoundToInt(_actors[i].transform.position.x / 10) && node.y == Mathf.RoundToInt(_actors[i].transform.position.z / 10)) {
                switch (skillType) {
                    default:
                        HealPerHitBuffActive(from, _actors[i].TakeDamage(from.Damage(), from.Weapon.item));
                        break;
                    case skills.DoubleLunge:
                        HealPerHitBuffActive(from, _actors[i].TakeDamage(from.Damage() * 2, from.Weapon.item));
                        break;
                    case skills.Cleave:
                        //_actors[i].Stun();
                        _actors[i].Status.ApplyStatus(buffsnDebuffs.Stunned);
                        break;
                    case skills.MoralizingShout:
                        if (_actors[i].transform.CompareTag("Player")) {
                            _actors[i].Status.ApplyStatus(buffsnDebuffs.MoralizingShoutBuff);
                        }
                        break;
                    case skills.Disarm:
                        //_actors[i].Stun();
                        _actors[i].Status.ApplyStatus(buffsnDebuffs.Disarmed);
                        break;
                    case skills.SedDeSangre:
                        //_actors[i].Stun();
                        _actors[i].Status.ApplyStatus(buffsnDebuffs.Bleed);
                        break;
                }


            }
        }

        from.EndAction();

    }

    void HealPerHitBuffActive(Actor from, int amount) {
        if (from.Status.isStatusActive(buffsnDebuffs.HealPerHit)) {
            from.SetHealth(amount);
        }
    }

    //pasa de cordenadas de mundo a grid
    bool ChechIfPositionIsInGrid(int i, int j) {
        return (!(i < 0 || j < 0 || i >= Stage.StageBuilder._grid.Rows || j >= Stage.StageBuilder._grid.Columns));
    }
    void GetPosToAttack(Actor actor, int range,out Node node, Node lastNode) 
    {
        node = lastNode;
        if (Stage.StageBuilder.MosueOverGrid())
        {

            GridPlane target = Stage.StageBuilder.GetMouseGridPlane();
            GridPlane origin = Stage.StageBuilder.GetGridPlane(actor.transform.position);

            if (Stage.StageBuilder.GetGridDistanceBetween(target, origin) <= range)
            {
                node = target.node;
            }
            Stage.StageBuilder.UpdateMaterial(node.x, node.y, shootMat);

            //actor.GridM().CalcRoute(actor.transform.position, Stage.StageBuilder.GetMouseGridPlane(), range);
            //node = Stage.StageBuilder.DisplayLastNodePath(actor.GridM().VisualRouteValid, range);

        }
    }
    void GetPosToEnemyAttack(Actor actor, out Node node)
    {
        //Actor player = FindNearestPlayer(actor);
        Actor player = Stage.StageManager.FindNearestPlayer(actor.transform);
        node = Stage.StageBuilder.GetGridPlane(Mathf.RoundToInt(player.transform.position.x / 10), Mathf.RoundToInt(player.transform.position.z / 10)).node;
        Stage.StageBuilder.GetGridPlane(node.x, node.y).SetMaterial(Stage.StageBuilder._rangeMath);    
    }

}
