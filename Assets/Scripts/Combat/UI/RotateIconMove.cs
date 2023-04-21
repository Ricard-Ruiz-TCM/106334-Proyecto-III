using UnityEngine;
using UnityEngine.EventSystems;

public class RotateIconMove : MonoBehaviour, IPointerClickHandler {
    [SerializeField] private int clickRotation = 0;
    [SerializeField] private GameObject perkOf1;
    [SerializeField] private GameObject perkOf2;
    [SerializeField] private GameObject perkDef1;
    [SerializeField] private GameObject perkDef2;

    private GameObject hoverAbility;
    private Quaternion rotation;
    private bool changed = false;

    public void OnPointerClick(PointerEventData eventData) {
        hoverAbility.GetComponent<RectTransform>().localRotation = Quaternion.Euler(new Vector3(rotation.x, rotation.y, rotation.z + clickRotation));
        rotation.z = rotation.z + 180;
        changed = !changed;
    }

    // Start is called before the first frame update
    void Start() {
        hoverAbility = gameObject;
        rotation = hoverAbility.GetComponent<RectTransform>().localRotation;
    }

    // Update is called once per frame
    void Update() {
        perkOf1.SetActive(!changed);
        perkOf2.SetActive(!changed);
        perkDef1.SetActive(changed);
        perkDef2.SetActive(changed);
    }
}
