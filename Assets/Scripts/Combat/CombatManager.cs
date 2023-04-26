using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    GridMovement _gridMovement;
    [SerializeField] LayerMask layerActor;
    private void Awake()
    {
        _gridMovement = GameObject.FindObjectOfType<GridMovement>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Prova (Actor actor, int range)
    {
        actor.canMove = false;
        StartCoroutine(Porro(actor, range));
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
                if (uCore.Action.GetKeyDown(KeyCode.G))
                {
                    canEnd = true;
                }
            }
            if (uCore.Action.GetKeyDown(KeyCode.H))
            {
                yield return null;
            }
            yield return new WaitForFixedUpdate();
        }
        GetActorInNode(node);
        _gridMovement.Builder().ClearGrid();
        actor.canMove = true;

    }
    private void GetActorInNode(Node node)
    {
        Debug.Log(node.x * 10 + "" + node.y * 10);
        RaycastHit hit;
        Debug.Break();
        Debug.DrawRay(new Vector3(node.x * 10, -10, node.y * 10), new Vector3(0, 1, 0), Color.green);
        if (Physics.Raycast(new Vector3(node.x * 10,-10,node.y * 10),new Vector3(0,1,0),out hit,100f, layerActor))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("enemyDetected");
            }
        }
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
                if (uCore.Action.GetKeyDown(KeyCode.G))
                {
                    canEnd = true;
                }
            }
            if (uCore.Action.GetKeyDown(KeyCode.H))
            {
                yield return null;
            }
            if(node != null)
            {
                for (int i = node.x-1; i < node.x + 1; i++)
                {
                    for (int j = node.y - 1; j < node.y + 1; j++)
                    {

                    }
                }
            }
            yield return new WaitForFixedUpdate();
        }
        GetActorInNode(node);
        _gridMovement.Builder().ClearGrid();
        actor.canMove = true;

    }
}
