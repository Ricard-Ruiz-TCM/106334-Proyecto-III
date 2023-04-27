using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    GridMovement _gridMovement;
    [SerializeField] LayerMask layerActor;
    [SerializeField] Material shootMat;
    private List<Actor> _actors;

    private void Awake()
    {
        _actors = new List<Actor>();
        _gridMovement = GameObject.FindObjectOfType<GridMovement>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public List<Actor> FindPlayers()
    {
        return _actors.FindAll(x => x is Player);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Add(Actor element)
    {
        if (!Contains(element))
            _actors.Add(element);

    }

    public void Remove(Actor element)
    {
        if (Contains(element))
            _actors.Remove(element);

    }

    private bool Contains(Actor element)
    {
        return _actors.Contains(element);
    }
    public void Prova(Actor actor, int range)
    {
        actor.canMove = false;
        StartCoroutine(Porro(actor, range));
    }
    public void Lanza(Actor actor, int range)
    {
        actor.canMove = false;
        StartCoroutine(Porro(actor, range));
    }
    public void GolpeDemoledor(Actor actor, int range)
    {
        actor.canMove = false;
        StartCoroutine(ShowGolpeDemoledor(actor, range));
    }

    IEnumerator ShowGolpeDemoledor(Actor actor, int range)
    {
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
                Debug.Log("J pressed");
                canEnd = true;
                GetActorInNode(node, actor, "cono");

            }
            if (node != null)
            {
                if (Mathf.RoundToInt(actor.transform.position.x / 10) == node.x)
                {
                    if (ChechIfPositionIsInGrid(node.x + 1, node.y))
                    {
                        _gridMovement.Builder().UpdateMaterial(node.x + 1, node.y, shootMat);
                        if (canEnd)
                        {
                            GetActorInNode(_gridMovement.Builder().GetGridPlane(node.x + 1, node.y).node, actor, "cono");
                        }

                    }
                    if (ChechIfPositionIsInGrid(node.x - 1, node.y))
                    {
                        _gridMovement.Builder().UpdateMaterial(node.x - 1, node.y, shootMat);
                        if (canEnd)
                        {
                            GetActorInNode(_gridMovement.Builder().GetGridPlane(node.x - 1, node.y).node, actor, "cono");
                        }

                    }
                    if (Mathf.RoundToInt(actor.transform.position.z / 10) > node.y)
                    {
                        if (ChechIfPositionIsInGrid(node.x, node.y - 1))
                        {
                            _gridMovement.Builder().UpdateMaterial(node.x, node.y - 1, shootMat);
                            if (canEnd)
                            {
                                GetActorInNode(_gridMovement.Builder().GetGridPlane(node.x, node.y - 1).node, actor, "cono");
                            }

                        }
                    }
                    else
                    {
                        if (ChechIfPositionIsInGrid(node.x, node.y + 1))
                        {
                            _gridMovement.Builder().UpdateMaterial(node.x, node.y + 1, shootMat);
                            if (canEnd)
                            {
                                GetActorInNode(_gridMovement.Builder().GetGridPlane(node.x, node.y + 1).node, actor, "cono");
                            }

                        }
                    }
                }



                if (Mathf.RoundToInt(actor.transform.position.z / 10) == node.y)
                {
                    if (ChechIfPositionIsInGrid(node.x, node.y + 1))
                    {
                        _gridMovement.Builder().UpdateMaterial(node.x, node.y + 1, shootMat);
                        if (canEnd)
                        {
                            GetActorInNode(_gridMovement.Builder().GetGridPlane(node.x, node.y + 1).node, actor, "cono");
                        }

                    }
                    if (ChechIfPositionIsInGrid(node.x, node.y - 1))
                    {
                        _gridMovement.Builder().UpdateMaterial(node.x, node.y - 1, shootMat);
                        if (canEnd)
                        {
                            GetActorInNode(_gridMovement.Builder().GetGridPlane(node.x, node.y - 1).node, actor, "cono");
                        }

                    }
                    if (Mathf.RoundToInt(actor.transform.position.x / 10) > node.x)
                    {
                        if (ChechIfPositionIsInGrid(node.x - 1, node.y))
                        {
                            _gridMovement.Builder().UpdateMaterial(node.x - 1, node.y, shootMat);
                            if (canEnd)
                            {
                                GetActorInNode(_gridMovement.Builder().GetGridPlane(node.x - 1, node.y).node, actor, "cono");
                            }

                        }
                    }
                    else
                    {
                        if (ChechIfPositionIsInGrid(node.x + 1, node.y))
                        {
                            _gridMovement.Builder().UpdateMaterial(node.x + 1, node.y, shootMat);
                            if (canEnd)
                            {
                                GetActorInNode(_gridMovement.Builder().GetGridPlane(node.x + 1, node.y).node, actor, "cono");
                            }

                        }
                    }


                }
            }
            if (uCore.Action.GetKeyDown(KeyCode.Escape))
            {
                canEnd = true;
            }
            yield return null;
        }
        _gridMovement.Builder().ClearGrid();
        actor.canMove = true;

    }

    IEnumerator Porro(Actor actor, int range)
    {
        bool canEnd = false;
        Node node = null;
        //List<Node> nodes = new List<Node>();


        while (!canEnd)
        {
            if (_gridMovement.Builder().MosueOverGrid())
            {
                _gridMovement.CalcRoute(actor.transform.position, _gridMovement.Builder().GetMouseGridPlane(), range);
                node = _gridMovement.Builder().DisplayLastNodePath(_gridMovement.VisualRouteValid, range);

            }
            if (uCore.Action.GetKeyDown(KeyCode.J))
            {
                Debug.Log("J pressed");
                canEnd = true;
                GetActorInNode(node, actor, "lanza");

            }
            if (uCore.Action.GetKeyDown(KeyCode.Escape))
            {
                canEnd = true;
            }
            yield return null;
        }
        _gridMovement.Builder().ClearGrid();
        actor.canMove = true;

    }
    private void GetActorInNode(Node node, Actor from, string attackType)
    {
        //Debug.Log(node.x * 10 + "" + node.y * 10);
        //RaycastHit hit;
        //Debug.Break();
        //Debug.DrawRay(new Vector3(node.x * 10, -10, node.y * 10), new Vector3(0, 1, 0), Color.green);
        //if (Physics.Raycast(new Vector3(node.x * 10,-10,node.y * 10),new Vector3(0,1,0),out hit,100f, layerActor))
        //{
        //    if (hit.transform.CompareTag("Enemy"))
        //    {
        //        Debug.Log("enemyDetected");
        //    }
        //}
        for (int i = 0; i < _actors.Count; i++)
        {
            if (node.x == Mathf.RoundToInt(_actors[i].transform.position.x / 10) && node.y == Mathf.RoundToInt(_actors[i].transform.position.z / 10))
            {
                switch (attackType)
                {
                    case "attack":
                        _actors[i].TakeDamage(from.Damage());
                        break;
                    case "arrows":
                        _actors[i].TakeDamage(from.Damage());
                        break;
                    case "lanza":
                        _actors[i].TakeDamage(from.Damage() * 2);
                        break;
                    case "cono":
                        _actors[i].Stun();
                        break;
                }


            }
        }

        from.EndAction();

    }
    public void ProvaArco(Actor actor, int range)
    {
        actor.canMove = false;
        StartCoroutine(Porro2(actor, range));
    }
    IEnumerator Porro2(Actor actor, int range)
    {
        bool canEnd = false;
        Node node = null;
        //List<Node> nodes = new List<Node>();


        while (!canEnd)
        {
            if (_gridMovement.Builder().MosueOverGrid())
            {
                _gridMovement.CalcRoute(actor.transform.position, _gridMovement.Builder().GetMouseGridPlane(), range);
                node = _gridMovement.Builder().DisplayLastNodePath(_gridMovement.VisualRouteValid, range);

            }
            if (uCore.Action.GetKeyDown(KeyCode.J))
            {
                GetActorInNode(node, actor, "arrows");
                canEnd = true;
            }
            if (node != null)
            {
                if (ChechIfPositionIsInGrid(node.x + 1, node.y))
                {
                    _gridMovement.Builder().UpdateMaterial(node.x + 1, node.y, shootMat);
                    if (canEnd)
                    {
                        GetActorInNode(_gridMovement.Builder().GetGridPlane(node.x + 1, node.y).node, actor, "arrows");
                    }

                }
                if (ChechIfPositionIsInGrid(node.x - 1, node.y))
                {
                    _gridMovement.Builder().UpdateMaterial(node.x - 1, node.y, shootMat);
                    if (canEnd)
                    {
                        GetActorInNode(_gridMovement.Builder().GetGridPlane(node.x - 1, node.y).node, actor, "arrows");
                    }
                }
                if (ChechIfPositionIsInGrid(node.x, node.y + 1))
                {
                    _gridMovement.Builder().UpdateMaterial(node.x, node.y + 1, shootMat);
                    if (canEnd)
                    {
                        GetActorInNode(_gridMovement.Builder().GetGridPlane(node.x, node.y + 1).node, actor, "arrows");
                    }
                }
                if (ChechIfPositionIsInGrid(node.x, node.y - 1))
                {
                    _gridMovement.Builder().UpdateMaterial(node.x, node.y - 1, shootMat);
                    if (canEnd)
                    {
                        GetActorInNode(_gridMovement.Builder().GetGridPlane(node.x, node.y - 1).node, actor, "arrows");
                    }
                }


            }
            if (uCore.Action.GetKeyDown(KeyCode.Escape))
            {
                canEnd = true;
            }
            yield return null;
        }

        _gridMovement.Builder().ClearGrid();
        actor.canMove = true;

    }
    bool ChechIfPositionIsInGrid(int i, int j)
    {
        return (!(i < 0 || j < 0 || i >= _gridMovement.Builder()._grid.Rows || j >= _gridMovement.Builder()._grid.Columns));
    }
}
