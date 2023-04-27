using UnityEngine;

public class StageScene : MonoBehaviour {

    public void BTN_Menu() {
        uCore.Director.LoadSceneAsync(gameScenes.Menu);
    }

    public void BTN_StageSelector() {
        uCore.Director.LoadSceneAsync(gameScenes.StageSelector);
    }

}
