using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InstanciateGrid : MonoBehaviour
{
    public int rows = 10;
    public int columns = 10;
    public int scale = 1;
    public GameObject gridPrefab;
    public float gridPrefabLenght = 10.5f;
    public Vector3 instanceGridPos = new Vector3(0, 0, 0);
    private Vector3 gridWorldSize;
    public LayerMask terrainLayer;

    [SerializeField] Node[,] grid;
    Inspector2dArray array;
    bool walkable;

    public Material walkableMat;
    public Material unwalkableMat;
    public Material pathMat;

    public GameObject player;

    public bool isClicked = false;
    private void Awake()
    {
        gridWorldSize = new Vector3(rows * gridPrefabLenght, 0, columns * gridPrefabLenght);
    }

    // Start is called before the first frame update
    void Start()
    {
        array = GetComponent<Inspector2dArray>();
        Instanciate();
        //StartCoroutine(resetMat());
    }
    //IEnumerator resetMat()
    //{
    //    yield return new WaitForSeconds(3f);
    //    for (int i = 0; i < rows; i++)
    //    {
    //        for (int j = 0; j < columns; j++)
    //        {
    //            if (grid[i, j].walkable)
    //            {
    //                grid[i, j].gridObj.GetComponent<MeshRenderer>().material = walkableMat;
    //            }

    //        }
    //    }
    //    StartCoroutine(resetMat());
    //}

    // Update is called once per frame
    void Update()
    {
        NodeFromWorldToPoint(player.transform.position).gridObj.GetComponent<MeshRenderer>().material = pathMat;
    }
    private void Instanciate()
    {
        grid = new Node[rows, columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject obj = GameObject.Instantiate(gridPrefab, new Vector3(instanceGridPos.x + scale * i * gridPrefabLenght, 100, instanceGridPos.z + scale * j * gridPrefabLenght), Quaternion.identity);
                RaycastHit raycastHit;

                if(Physics.Raycast(obj.transform.position,new Vector3(0,-1,0),out raycastHit, Mathf.Infinity, terrainLayer))
                {
                    obj.transform.position = new Vector3(raycastHit.point.x, raycastHit.point.y + 0.1f, raycastHit.point.z);
                    obj.transform.rotation = Quaternion.FromToRotation(transform.up, raycastHit.normal);
                }

                if (!array.columns[j].rows[i])
                {
                    walkable = true;
                    obj.GetComponent<MeshRenderer>().material = walkableMat;
                }
                else
                {
                    obj.GetComponent<MeshRenderer>().material = unwalkableMat;
                    walkable = false;
                }
                grid[i, j] = new Node(walkable, obj.transform.position,obj,i,j);    


                //if (Physics.Raycast(new Vector3(instanceGridPos.x + scale * i * gridPrefabLenght, 1, instanceGridPos.z + scale * j * gridPrefabLenght),new Vector3(0,1,0), 10, rayLayerMask))
                //{
                //    Debug.Log("obstacle");
                //}
                //else
                //{
                //    Debug.Log("no obstacle");
                //    GameObject.Instantiate(gridPrefab,new Vector3(instanceGridPos.x + scale * i * gridPrefabLenght, 1, instanceGridPos.z + scale * j * gridPrefabLenght), Quaternion.identity);
                //}
            }
                
        }
        
    }

    public Node NodeFromWorldToPoint(Vector3 worldPos)
    {
        Vector3 relativePoint = grid[0,0].gridObj.transform.InverseTransformPoint(worldPos);
        int x = Mathf.RoundToInt(relativePoint.x / 10);
        int z = Mathf.RoundToInt(relativePoint.z / 10);

        return grid[(int)x, (int)z];
    }
    public void SetWalkablePath(List<Node> path)
    {
        foreach (Node n in grid)
        {
            if (path.Contains(n))
            {
                n.gridObj.GetComponent<MeshRenderer>().material = pathMat;
            }
            else if(n.walkable)
            {
                n.gridObj.GetComponent<MeshRenderer>().material = walkableMat;
            }
        }
        if (isClicked)
        {
            player.GetComponent<MoveProva>().ResetPath(path);
            isClicked = false;
        }
        
    }
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        int checkX = node.gridX + 1;
        int checkY = node.gridY;

        if (checkX >= 0 && checkX < rows - 1 && checkY >= 0 && checkY < columns - 1)
        {
            neighbours.Add(grid[checkX, checkY]);
        }


        checkX = node.gridX - 1;
        checkY = node.gridY;

        if (checkX >= 0 && checkX < rows - 1 && checkY >= 0 && checkY < columns - 1)
        {
            neighbours.Add(grid[checkX, checkY]);
        }

        checkX = node.gridX;
        checkY = node.gridY + 1;

        if (checkX >= 0 && checkX < rows - 1 && checkY >= 0 && checkY < columns - 1)
        {
            neighbours.Add(grid[checkX, checkY]);
        }
        checkX = node.gridX;
        checkY = node.gridY - 1;

        if (checkX >= 0 && checkX < rows - 1 && checkY >= 0 && checkY < columns - 1)
        {
            neighbours.Add(grid[checkX, checkY]);
        }



        return neighbours;
    }
}
