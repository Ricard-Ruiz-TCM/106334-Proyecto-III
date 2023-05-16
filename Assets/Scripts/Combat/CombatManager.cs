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

    public List<Turnable> FindPlayers() {
        return _actors.FindAll(x => x is Player);
    }
    public List<Turnable> FindEnemys() {
        return _actors.FindAll(x => x is Enemy);
    }

    //metodos para hacer las skills
    //los que no estan aqui se hacen desde su scriptableObject, eso es porque la skill se hace sin tener que seleccionar ninguna casilla

    public void UseSkill(Turnable actor, skillID skillType, bool canInteract) {
        actor.canMove = false;

        int range = actor.Weapon.range;

        switch (skillType) {
            case skillID.Attack:
                StartCoroutine(NormalAttack(actor, range, skillType, canInteract));
                break;
            case skillID.DoubleLunge:
                StartCoroutine(NormalAttack(actor, range, skillType, canInteract));
                break;
            case skillID.Cleave:
                StartCoroutine(ShowGolpeDemoledor(actor, range, skillType, canInteract));
                break;
            case skillID.ArrowRain:
                StartCoroutine(ShowArrows(actor, range, skillType, canInteract));
                break;
            case skillID.ImperialCry:
                StartCoroutine(ShowMoralizingShout(actor, range, skillType));
                break;
            case skillID.Disarm:
                StartCoroutine(NormalAttack(actor, range, skillType, canInteract));
                break;
            case skillID.Bloodlust:
                StartCoroutine(NormalAttack(actor, range, skillType, canInteract));
                break;
        }

    }

    //enumerators que funcionan como "updates" para hacer las funciones de las skills
    IEnumerator ShowGolpeDemoledor(Turnable actor, int range, skillID skillType, bool canInteract) {
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
        } else {
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
    private void ExtendGolpeDemoledor(Turnable actor, int range, skillID skillType, Node node, bool canEnd) {
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

    IEnumerator NormalAttack(Turnable actor, int range, skillID skillType, bool canInteract) {
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
        } else {
            GetPosToEnemyAttack(actor, out node);
            if (node != null) {
                GetActorInNode(node, actor, skillType);
            }
        }
        StartCoroutine(EndAttack(actor));

    }

    IEnumerator ShowArrows(Turnable actor, int range, skillID skillType, bool canInteract) {
        bool canEnd = false;
        Node node = null;
        //List<Node> nodes = new List<Node>();

        if (canInteract) {
            while (!canEnd) {

                Stage.StageBuilder.ClearGrid();

                GetPosToAttack(actor, range, out node, node);

                if (uCore.Action.GetKeyDown(KeyCode.J)) 
                {
                    uCore.Particles.PlayParticles("ParticulasFlechas", new Vector3(node.x * 10, 19, node.y * 10)).destroyAtEnd();
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
        } else {
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
    IEnumerator ShowMoralizingShout(Turnable actor, int range, skillID skillType) {
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
    IEnumerator EndAttack(Turnable actor) {
        yield return new WaitForSeconds(1f);
        Stage.StageBuilder.ClearGrid();
        actor.canMove = true;
    }

    //metodo para los ataques que tienen da�o en area (comprueba si esta dentro de la grid - cambia el material - y si hay un enemigo hace efecto)
    private void ExtendAttack(int i, int j, Turnable actor, bool canEnd, skillID skillType) {
        if (ChechIfPositionIsInGrid(i, j)) {
            Stage.StageBuilder.UpdateMaterial(i, j, shootMat);
            if (canEnd) {
                GetActorInNode(Stage.StageBuilder.GetGridPlane(i, j).node, actor, skillType);
            }

        }
    }

    //metodo para checkear si hay un actor en un nodo y aplica la skill en el actor
    private void GetActorInNode(Node node, Turnable from, skillID skillType) {
        for (int i = 0; i < _actors.Count; i++) {
            if (node.x == Mathf.RoundToInt(_actors[i].transform.position.x / 10) && node.y == Mathf.RoundToInt(_actors[i].transform.position.z / 10)) {
                switch (skillType) {
                    default:
                        HealPerHitBuffActive(from, _actors[i].TakeDamage(from.Damage(), from.Weapon.item));
                        break;
                    case skillID.ArrowRain:
                        HealPerHitBuffActive(from, _actors[i].TakeDamage(from.Damage(), from.Weapon.item));
                        break;
                    case skillID.DoubleLunge:
                        HealPerHitBuffActive(from, _actors[i].TakeDamage(from.Damage() * 2, from.Weapon.item));
                        break;
                    case skillID.Cleave:
                        //_actors[i].Stun();
                        _actors[i].Status.ApplyStatus(buffsID.Stunned);
                        break;
                    case skillID.ImperialCry:
                        if (_actors[i].transform.CompareTag("Player")) {
                            _actors[i].Status.ApplyStatus(buffsID.Motivated);
                        }
                        break;
                    case skillID.Disarm:
                        //_actors[i].Stun();
                        _actors[i].Status.ApplyStatus(buffsID.Disarmed);
                        break;
                    case skillID.Bloodlust:
                        //_actors[i].Stun();
                        _actors[i].Status.ApplyStatus(buffsID.Bleeding);
                        break;
                }


            }
        }

        from.EndAction();

    }

    void HealPerHitBuffActive(Turnable from, int amount) {
        //if (from.Status.isStatusActive(buffsID.HealPerHit)) {
            from.SetHealth(amount);
        //}
    }

    //pasa de cordenadas de mundo a grid
    bool ChechIfPositionIsInGrid(int i, int j) {
        return (!(i < 0 || j < 0 || i >= Stage.StageBuilder._grid.Rows || j >= Stage.StageBuilder._grid.Columns));
    }
    void GetPosToAttack(Turnable actor, int range, out Node node, Node lastNode) {
        node = lastNode;
        if (Stage.StageBuilder.MosueOverGrid()) {

            GridPlane target = Stage.StageBuilder.GetMouseGridPlane();
            GridPlane origin = Stage.StageBuilder.GetGridPlane(actor.transform.position);

            if (Stage.StageBuilder.GetGridDistanceBetween(target, origin) <= range) {
                node = target.node;
            }
            if (node != null) {
                Stage.StageBuilder.UpdateMaterial(node.x, node.y, shootMat);
            }

            //actor.GridM().CalcRoute(actor.transform.position, Stage.StageBuilder.GetMouseGridPlane(), range);
            //node = Stage.StageBuilder.DisplayLastNodePath(actor.GridM().VisualRouteValid, range);

        }
    }
    void GetPosToEnemyAttack(Turnable actor, out Node node) {
        //Actor player = FindNearestPlayer(actor);
        Turnable player = Stage.StageManager.FindNearestPlayer(actor.transform);
        node = Stage.StageBuilder.GetGridPlane(Mathf.RoundToInt(player.transform.position.x / 10), Mathf.RoundToInt(player.transform.position.z / 10)).node;
        Stage.StageBuilder.GetGridPlane(node.x, node.y).SetMaterial(Stage.StageBuilder._rangeMath);
    }

}
