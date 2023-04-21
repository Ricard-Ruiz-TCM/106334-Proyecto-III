using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectGridSpot : MonoBehaviour
{
    RaycastHit raycastHit;
    [SerializeField] Camera camera;
    [SerializeField] Pathfinding pathfinding;
    [SerializeField] Transform player;
    [SerializeField] InstanciateGrid grid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if(!EventSystem.current.IsPointerOverGameObject()&&Physics.Raycast(ray,out raycastHit))
        {
            if (raycastHit.transform.CompareTag("grid"))
            {
                Debug.Log("gridSelected");
                pathfinding.FindPath(player.transform.position,raycastHit.point);
                if (uCore.Action.GetKeyDown(KeyCode.Space))
                {
                    grid.isClicked = true;
                }
            }
        }
    }
}
