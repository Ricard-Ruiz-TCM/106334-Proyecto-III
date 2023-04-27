using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {
    GridMovement _gridMovement;
    [SerializeField] LayerMask layerActor;
    [SerializeField] Material shootMat;
    private List<Actor> _actors;

    private void Awake() {
        _actors = new List<Actor>();
        _gridMovement = GameObject.FindObjectOfType<GridMovement>();
    }
    // Start is called before the first frame update
    void Start() {

    }

    public List<Actor> FindPlayers() {
        return _actors.FindAll(x => x is Player);
    }

    // Update is called once per frame
    void Update() {

    }
    //añadir o remover actores de una lista (lo hacemos para controlar en que posicion se encuentran los actores y aplicarle efectos)
    public void Add(Actor element) {
        if (!Contains(element))
            _actors.Add(element);

    }
    public void Remove(Actor element) {
        if (Contains(element))
            _actors.Remove(element);

    }
    private bool Contains(Actor element) {
        return _actors.Contains(element);
    }


    //metodos para hacer las skills
    //los que no estan aqui se hacen desde su scriptableObject, eso es porque la skill se hace sin tener que seleccionar ninguna casilla
    public void Attack(Actor actor, int range, skills skillType) {
        actor.canMove = false;
        StartCoroutine(NormalAttack(actor, range, skillType));
    }
    public void Lanza(Actor actor, int range, skills skillType) {
        actor.canMove = false;
        StartCoroutine(NormalAttack(actor, range, skillType));
    }
    public void GolpeDemoledor(Actor actor, int range, skills skillType) {
        actor.canMove = false;
        StartCoroutine(ShowGolpeDemoledor(actor, range, skillType));
    }
    public void Arco(Actor actor, int range, skills skillType)
    {
        actor.canMove = false;
        StartCoroutine(ShowArrows(actor, range, skillType));
    }


    //enumerators que funcionan como "updates" para hacer las funciones de las skills
    IEnumerator ShowGolpeDemoledor(Actor actor, int range, skills skillType) {
        bool canEnd = false;
        Node node = null;

        while (!canEnd)
        {
            if (_gridMovement.Builder().MosueOverGrid())
            {
                _gridMovement.CalcRoute(actor.transform.position, _gridMovement.Builder().GetMouseGridPlane(), range);
                node = _gridMovement.Builder().DisplayLastNodePath(_gridMovement.VisualRouteValid, range);

            }
            if (uCore.Action.GetKeyDown(KeyCode.J))
            {
                canEnd = true;
                GetActorInNode(node, actor, skillType);

            }
            if (node != null) 
            {
                if (Mathf.RoundToInt(actor.transform.position.x / 10) == node.x) 
                {
                    ExtendAttack(node.x + 1, node.y, actor, canEnd, skillType);

                    ExtendAttack(node.x - 1, node.y, actor, canEnd, skillType);

                    if (Mathf.RoundToInt(actor.transform.position.z / 10) > node.y) 
                    {
                        ExtendAttack(node.x, node.y - 1, actor, canEnd, skillType);                        
                    } 
                    else 
                    {
                        ExtendAttack(node.x, node.y + 1, actor, canEnd, skillType);
                    }
                }



                if (Mathf.RoundToInt(actor.transform.position.z / 10) == node.y) 
                {
                    ExtendAttack(node.x , node.y + 1, actor, canEnd, skillType);

                    ExtendAttack(node.x, node.y - 1, actor, canEnd, skillType);

                    if (Mathf.RoundToInt(actor.transform.position.x / 10) > node.x) 
                    {
                        ExtendAttack(node.x - 1, node.y, actor, canEnd, skillType);
                    } 
                    else 
                    {
                        ExtendAttack(node.x + 1, node.y, actor, canEnd, skillType);
                    }


                }
            }
            if (uCore.Action.GetKeyDown(KeyCode.Escape)) {
                canEnd = true;
            }
            yield return null;
        }
        _gridMovement.Builder().ClearGrid();
        actor.canMove = true;
    }

    IEnumerator NormalAttack(Actor actor, int range, skills skillType) {
        bool canEnd = false;
        Node node = null;


        while (!canEnd) {
            if (_gridMovement.Builder().MosueOverGrid()) {
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
        _gridMovement.Builder().ClearGrid();
        actor.canMove = true;

    }
  
    IEnumerator ShowArrows(Actor actor, int range, skills skillType) {
        bool canEnd = false;
        Node node = null;
        //List<Node> nodes = new List<Node>();


        while (!canEnd) {
            if (_gridMovement.Builder().MosueOverGrid()) {
                _gridMovement.CalcRoute(actor.transform.position, _gridMovement.Builder().GetMouseGridPlane(), range);
                node = _gridMovement.Builder().DisplayLastNodePath(_gridMovement.VisualRouteValid, range);

            }
            if (uCore.Action.GetKeyDown(KeyCode.J)) {
                GetActorInNode(node, actor, skillType);
                canEnd = true;
            }
            if (node != null) 
            {
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

        _gridMovement.Builder().ClearGrid();
        actor.canMove = true;

    }

    //metodo para los ataques que tienen daño en area (comprueba si esta dentro de la grid - cambia el material - y si hay un enemigo hace efecto)
    private void ExtendAttack(int i, int j, Actor actor, bool canEnd, skills skillType)
    {
        if (ChechIfPositionIsInGrid(i, j))
        {
            _gridMovement.Builder().UpdateMaterial(i, j, shootMat);
            if (canEnd)
            {
                GetActorInNode(_gridMovement.Builder().GetGridPlane(i, j).node, actor, skillType);
            }

        }
    }

    //metodo para checkear si hay un actor en un nodo y aplica la skill en el actor
    private void GetActorInNode(Node node, Actor from, skills skillType)
    {
        for (int i = 0; i < _actors.Count; i++)
        {
            if (node.x == Mathf.RoundToInt(_actors[i].transform.position.x / 10) && node.y == Mathf.RoundToInt(_actors[i].transform.position.z / 10))
            {
                switch (skillType)
                {
                    default:
                        _actors[i].TakeDamage(from.Damage());
                        break;
                    case skills.DoubleLunge:
                        _actors[i].TakeDamage(from.Damage() * 2);
                        break;
                    case skills.Cleave:
                        _actors[i].Stun();
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
