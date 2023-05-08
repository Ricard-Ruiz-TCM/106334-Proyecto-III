using UnityEngine;

public class SceneStageSelectorManager : MonoBehaviour {
    /** Buttons */
    public void BTN_Back() {
        uCore.Director.LoadSceneAsync(gameScenes.Menu);
    }
    /** ------- */
}
