using UnityEngine;

public class MenuController : MonoBehaviour {

    public void BTN_Play() {
        uCore.Director.LoadSceneAsync(gameScenes.StageSelector);
    }

    public void BTN_Settings() {

    }

    public void BTN_Exit() {
        Application.Quit(0);
    }

}
