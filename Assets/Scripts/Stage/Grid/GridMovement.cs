using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GridMovement : MonoBehaviour {
    private NavMeshAgent _agent;

    private Pathfinding _pathfinder;
    private GridBuilder _builder;

    public Material pathMat;


    List<Node> playerPath;
    int index = 0;
    public bool canMove = false;

    private void Awake() {
        _agent = GetComponent<NavMeshAgent>();
        _builder = GetComponent<GridBuilder>();
    }

    // Start is called before the first frame update
    void Start() {
        _pathfinder = GameObject.FindObjectOfType<Pathfinding>();
        _builder = GameObject.FindObjectOfType<GridBuilder>();
        pathMat = Resources.Load<Material>("Materials/PathMat");
    }

    public void ResetPath(List<Node> path) {
        playerPath = path;
        index = 0;
        canMove = false;
    }

    // Update is called once per frame
    void Update() {
        INput();
        if (playerPath != null && canMove) {
            NextPathNode();
            _agent.SetDestination(_builder.GetGridPlane(playerPath[index]).position);
        }

    }
    private void NextPathNode() {
        if (transform.position.x == (_builder.GetGridPlane(playerPath[index]).position).x && transform.position.z == (_builder.GetGridPlane(playerPath[index]).position).z) {
            index++;
            if (index == playerPath.Count) {
                playerPath = null;
                canMove = false;
            }
        }
    }


    public void INput() {
        /*RaycastHit raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.transform.CompareTag("grid"))
            {
                Node dest = raycastHit.transform.GetComponent<GridPlane>().node;
                ray = new Ray(transform.position, -this.transform.up);

                if (Physics.Raycast(ray, out raycastHit)) 
                {
                    playerPath = _pathfinder.FindPath(raycastHit.transform.gameObject.GetComponent<GridPlane>().node, dest);
                    canMove = uCore.Action.GetKeyDown(KeyCode.Space);
                }
            }
        }*/
        if (uCore.Action.GetKeyDown(KeyCode.P)) {
            playerPath = _pathfinder.FindPath(_builder.GetGridPlane(1, 1).node, _builder.GetGridPlane(6, 6).node);
            canMove = true;
        }
    }
}
