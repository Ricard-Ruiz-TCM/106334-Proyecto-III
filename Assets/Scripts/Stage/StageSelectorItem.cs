using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageSelectorItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField]
    private StageData _data;
    public StageData Data => _data;
    
    [SerializeField, Header("UI Purpouse:")]
    public StageSelectorPanelInfoUI _panelInfo;
    [SerializeField]
    private bool _visible;
    [SerializeField]
    private Sprite _panelPreview;

    [SerializeField, Header("Next Stage:")]
    private GameObject[] _nextSteps;

    // Unity OnDrawGizmos
    void OnDrawGizmos() {
        foreach (GameObject go in _nextSteps) {
            Gizmos.DrawLine(transform.position, go.transform.position);
        }
    }

    // Unity Awake
    void Awake() {
        gameObject.SetActive(uCore.GameManager.StageRecord.Contains(_data) || _data.ID == uCore.GameManager.StageID || _visible);
    }

    // Unity Start
    void Start() {
        if (_data.ID != uCore.GameManager.StageID)
            return;

        foreach (GameObject go in _nextSteps) {
            go.GetComponent<Button>().interactable = true;
            if (!go.activeSelf) {
                go.SetActive(true);
            }
        }
    }

    /** Método para entar al stage */
    public void BTN_EnterStage() {
        uCore.GameManager.StageSelected(_data);
        uCore.Director.LoadSceneAsync(gameScenes.Stage);
    }

    // OnPointerEnter
    public void OnPointerEnter(PointerEventData eventData) {
        _panelInfo.gameObject.SetActive(true);
        _panelInfo.Set(_data, _panelPreview);
    }

    // OnPointerExit
    public void OnPointerExit(PointerEventData eventData) {
        _panelInfo.gameObject.SetActive(false);
    }

}
