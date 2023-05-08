using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {

    public static CombatManager instance = null;

    GridMovement _gridMovement;
    [SerializeField] LayerMask layerActor;
    [SerializeField] Material shootMat;
    private List<Actor> _actors;

    private void Awake() {

        // Singleton
        if ((instance != null) && (instance != this)) {
            GameObject.Destroy(this.gameObject);
        } else {
            instance = this;
        }

        _actors = new List<Actor>();
        _gridMovement = GameObject.FindObjectOfType<GridMovement>();
    }
    // Start is called before the first frame update
    void Start() {

    }

    public List<Actor> FindPlayers() {
        return _actors.FindAll(x => x is Player);
    }

    //a�adir o remover actores de una lista (lo hacemos para controlar en que posicion se encuentran los actores y aplicarle efectos)
    public void Subscribe(Actor element) {
        if (!Contains(element))
            _actors.Add(element);

    }
    public void Unsubscribe(Actor element) {
        if (Contains(element))
            _actors.Remove(element);

    }
    private bool Contains(Actor element) {
        return _actors.Contains(element);
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
        }

    }

    //enumerators que funcionan como "updates" para hacer las funciones de las skills
    IEnumerator ShowGolpeDemoledor(Actor actor, int range, skills skillType, bool canInteract) {
        bool canEnd = false;
        Node node = null;

        if (canInteract) {
            while (!canEnd) {
                if (_gridMovement.Builder().MosueOverGrid()) {
                    _gridMovement.CalcRoute(actor.transform.position, _gridMovement.Builder().GetMouseGridPlane(), range);
                    node = _gridMovement.Builder().DisplayLastNodePath(_gridMovement.VisualRouteValid, range);
                }
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
            Actor player = FindNearest();
            node = _gridMovement.Builder().GetGridPlane(Mathf.RoundToInt(player.transform.position.x / 10), Mathf.RoundToInt(player.transform.position.z / 10)).node;
            _gridMovement.Builder().GetGridPlane(node.x, node.y).SetMaterial(_gridMovement.Builder()._rangeMath);
            if (node != null) {
                GetActorInNode(node, actor, skillType);
                ExtendGolpeDemoledor(actor, range, skillType, node, false);
            }
        }

        StartCoroutine(EndAttack(actor));
    }
    public Actor FindNearest() {

        Actor actor = null;
        float dist = Mathf.Infinity;

        foreach (Actor obj in FindPlayers()) {
            float distance = Vector3.Distance(obj.transform.position, transform.position);
            if (distance < dist) {
                if (!obj.IsInvisible()) {
                    actor = obj;
                    dist = distance;
                }
            }
        }

        return actor;
    }
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
                if (Stage.StageBuilder.MosueOverGrid()) {
                    _gridMovement.CalcRoute(actor.transform.position, _gridMovement.Builder().GetMouseGridPlane(), range);
                    node = _gridMovement.Builder().DisplayLastNodePath(_gridMovement.VisualRouteValid, range);

                }
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
            Actor player = FindNearest();
            node = _gridMovement.Builder().GetGridPlane(Mathf.RoundToInt(player.transform.position.x / 10), Mathf.RoundToInt(player.transform.position.z / 10)).node;
            _gridMovement.Builder().GetGridPlane(node.x, node.y).SetMaterial(_gridMovement.Builder()._rangeMath);
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
                if (_gridMovement.Builder().MosueOverGrid()) {
                    _gridMovement.CalcRoute(actor.transform.position, _gridMovement.Builder().GetMouseGridPlane(), range);
                    node = _gridMovement.Builder().DisplayLastNodePath(_gridMovement.VisualRouteValid, range);

                }
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
        } else {
            Actor player = FindNearest();
            node = _gridMovement.Builder().GetGridPlane(Mathf.RoundToInt(player.transform.position.x / 10), Mathf.RoundToInt(player.transform.position.z / 10)).node;
            _gridMovement.Builder().GetGridPlane(node.x, node.y).SetMaterial(_gridMovement.Builder()._rangeMath);
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


        node = _gridMovement.Builder().GetGridPlane(Mathf.RoundToInt(actor.transform.position.x / 10), Mathf.RoundToInt(actor.transform.position.z / 10)).node;

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
        _gridMovement.Builder().ClearGrid();
        actor.canMove = true;
    }

    //metodo para los ataques que tienen da�o en area (comprueba si esta dentro de la grid - cambia el material - y si hay un enemigo hace efecto)
    private void ExtendAttack(int i, int j, Actor actor, bool canEnd, skills skillType) {
        if (ChechIfPositionIsInGrid(i, j)) {
            _gridMovement.Builder().UpdateMaterial(i, j, shootMat);
            if (canEnd) {
                GetActorInNode(_gridMovement.Builder().GetGridPlane(i, j).node, actor, skillType);
            }

        }
    }

    //metodo para checkear si hay un actor en un nodo y aplica la skill en el actor
    private void GetActorInNode(Node node, Actor from, skills skillType) {
        for (int i = 0; i < _actors.Count; i++) {
            if (node.x == Mathf.RoundToInt(_actors[i].transform.position.x / 10) && node.y == Mathf.RoundToInt(_actors[i].transform.position.z / 10)) {
                switch (skillType) {
                    default:
                        _actors[i].TakeDamage(from.Damage(), from.Weapon.item);
                        break;
                    case skills.DoubleLunge:
                        _actors[i].TakeDamage(from.Damage() * 2, from.Weapon.item);
                        break;
                    case skills.Cleave:
                        //_actors[i].Stun();
                        break;
                    case skills.MoralizingShout:
                        if (_actors[i].transform.CompareTag("Player")) {
                            _actors[i].UpdateAttack(2, "+", 1);
                            Debug.Log("mola");
                        }
                        break;
                }


            }
        }

        from.EndAction();

    }

    //pasa de cordenadas de mundo a grid
    bool ChechIfPositionIsInGrid(int i, int j) {
        return (!(i < 0 || j < 0 || i >= _gridMovement.Builder()._grid.Rows || j >= _gridMovement.Builder()._grid.Columns));
    }
}
