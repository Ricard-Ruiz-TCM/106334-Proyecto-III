using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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

    public GameObject player;
    private void Awake()
    {
        gridWorldSize = new Vector3(rows * gridPrefabLenght, 0, columns * gridPrefabLenght);
        Debug.Log(gridWorldSize);
    }

    // Start is called before the first frame update
    void Start()
    {
        array = GetComponent<Inspector2dArray>();
        Instanciate();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(NodeFromWorldToPoint(player.transform.position).worldPos);
        NodeFromWorldToPoint(player.transform.position).gridObj.GetComponent<MeshRenderer>().material = unwalkableMat;
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
                    walkable = false;
                    obj.GetComponent<MeshRenderer>().material = walkableMat;
                }
                else
                {
                    obj.GetComponent<MeshRenderer>().material = unwalkableMat;
                    walkable = true;
                }
                grid[i, j] = new Node(walkable, obj.transform.position,obj);    


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

}
