using UnityEngine;
using UnityEngine.UI;

public class StageSelectorPanelInfoUI : MonoBehaviour {

    [SerializeField, Header("Icons Prefabs:")]
    private GameObject[] _difficilty;
    [SerializeField]
    private GameObject[] _type;
    [SerializeField]
    private GameObject[] _terrain;

    [SerializeField, Header("Panel Elements:")]
    private UIText _txtName;
    [SerializeField]
    private Image _imgPreview;
    [SerializeField]
    private Transform _panelIcons;

    // Unity Awake
    void Awake() {
        gameObject.SetActive(false);
    }

    /** Método para setear toda la información del stage */
    public void Set(StageData data, Sprite preview = null) {
        // Text Name
        _txtName.SetKey(data.keyName);
        // Icons childs
        foreach (Transform child in _panelIcons) {
            GameObject.Destroy(child.gameObject);
        }
        // Instant new Childs
        GameObject.Instantiate(_difficilty[(int)data.diff], _panelIcons);
        GameObject.Instantiate(_type[(int)data.type], _panelIcons);
        GameObject.Instantiate(_terrain[(int)data.terrain], _panelIcons);
        // Set de Preview
        if (preview != null)
            _imgPreview.sprite = preview;
    }

}
