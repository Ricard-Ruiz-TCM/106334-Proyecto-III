using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Grid))]
public class GridBuilder : MonoBehaviour
{

    private Grid grid;
    private GridPlane[,] gridPlane;
    public GameObject gridPlanePrefab;
    public float gridPlaneLenght = 10f;


    public LayerMask terrainLayer;


    public Material walkableMat;
    public Material unwalkableMat;

    private void Awake()
    {
        grid = GetComponent<Grid>();

    }

    // Start is called before the first frame update
    void Start()
    {       
        Instanciate();
    }

    public GridPlane GetGridPlane(Node node)
    {
        return gridPlane[node.gridX, node.gridY];
    }

    public GridPlane GetGridPlane(int x, int y)
    {
        return gridPlane[x, y];
    }

    private void Instanciate()
    {
        gridPlane = new GridPlane[grid.rows, grid.columns];
        for (int x = 0; x < grid.rows; x++)
        {
            for (int y = 0; y < grid.columns; y++)
            {
                GridPlane obj = GameObject.Instantiate(gridPlanePrefab, new Vector3(gridPlaneLenght * x * gridPlanePrefab.transform.localScale.x, 100, gridPlaneLenght * y * gridPlanePrefab.transform.localScale.y), Quaternion.identity).GetComponent<GridPlane>();
                RaycastHit raycastHit;

                if(Physics.Raycast(obj.transform.position,new Vector3(0,-1,0),out raycastHit, Mathf.Infinity, terrainLayer))
                {
                    obj.transform.position = new Vector3(raycastHit.point.x, raycastHit.point.y + 0.1f, raycastHit.point.z);
                    obj.transform.rotation = Quaternion.FromToRotation(transform.up, raycastHit.normal);
                }

                if (grid.GridMap()[x, y].walkable)
                {
                    obj.SetMaterial(walkableMat);
                } else
                {
                    obj.SetMaterial(unwalkableMat);
                }
                obj.Set(grid, grid.GetNode(x, y));
                gridPlane[x, y] = obj;

            }
                
        }
        
    }

    /*public Node NodeFromWorldToPoint(Vector3 worldPos)
    {
        Vector3 relativePoint = grid[0,0].gridObj.transform.InverseTransformPoint(worldPos);

        int x = Mathf.RoundToInt(relativePoint.x / 10);
        int z = Mathf.RoundToInt(relativePoint.z / 10);

        return grid[(int)x, (int)z];
    }*/

   
}
