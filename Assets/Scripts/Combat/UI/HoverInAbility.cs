using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverInAbility : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject hoverAbility;
    [SerializeField] private int hoverSpace = 0;

    public void OnPointerEnter(PointerEventData eventData)
    {
        var position = hoverAbility.GetComponent<RectTransform>().localPosition;
        hoverAbility.GetComponent<RectTransform>().localPosition = new Vector3(position.x, position.y + hoverSpace, position.z);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var position = hoverAbility.GetComponent<RectTransform>().localPosition;
        hoverAbility.GetComponent<RectTransform>().localPosition = new Vector3(position.x, position.y - hoverSpace, position.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        hoverAbility = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
