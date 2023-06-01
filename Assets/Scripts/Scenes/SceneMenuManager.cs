using UnityEngine;

public class SceneMenuManager : MonoBehaviour {


    /** Buttons */
    public void BTN_NewGame() {
        uCore.Director.LoadSceneAsync(gameScenes.Stage0);
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
