using UnityEngine;

public class SceneMenuManager : MonoBehaviour {

    [SerializeField, Header("Tutorial Stage:")]
    private StageData _tutorial;

    [SerializeField, Header("Continue Button:")]
    private GameObject _continue;

    // Unity Awake
    void Awake() {
        if (!uCore.GameManager.ExistGameData()) {
            _continue.SetActive(false);
        }
    }

    /** Buttons */
    public void BTN_Continue() {
        uCore.GameManager.LoadGameData();
        uCore.Director.LoadSceneAsync(gameScenes.StageSelector);
    }

    public void BTN_NewGame() {
        uCore.GameManager.StageSelected(_tutorial);
        uCore.Director.LoadSceneAsync(gameScenes.Stage);
    }

    public void BTN_Settings() {
        Debug.Log("Options");
    }

    public void BTN_Credits() {
        uCore.Director.LoadSceneAsync(gameScenes.Credits);
    }

    public void BTN_Exit() {
        Application.Quit(0);
    }
    /** ------- */

}
