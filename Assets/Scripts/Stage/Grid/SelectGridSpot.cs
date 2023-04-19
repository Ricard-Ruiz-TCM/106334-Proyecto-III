using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectGridSpot : MonoBehaviour
{
    RaycastHit raycastHit;
    [SerializeField] Camera camera;
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
            }
        }
    }
}
