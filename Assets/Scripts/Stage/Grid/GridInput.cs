using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class GridInput : MonoBehaviour
{
    RaycastHit raycastHit;
    [SerializeField] Camera camera;
    [SerializeField] Pathfinding pathfinding;
    [SerializeField] Transform player;
    [SerializeField] GridBuilder grid;

    public Material pathMat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
