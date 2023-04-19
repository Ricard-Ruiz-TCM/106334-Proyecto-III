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
    [SerializeField] int[,] gridArray;
    public int startX;
    public int startY;
    public int endY;
    public int endX;
    [SerializeField] LayerMask rayLayerMask;
    Inspector2dArray array;

    // Start is called before the first frame update
    void Start()
    {
        array = GetComponent<Inspector2dArray>();
        Instanciate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Instanciate()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if(!array.columns[j].rows[i])
                {
                    GameObject.Instantiate(gridPrefab, new Vector3(instanceGridPos.x + scale * i * gridPrefabLenght, 1, instanceGridPos.z + scale * j * gridPrefabLenght), Quaternion.identity);
                }
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
}
