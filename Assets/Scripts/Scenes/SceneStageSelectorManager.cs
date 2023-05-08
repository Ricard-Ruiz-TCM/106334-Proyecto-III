using UnityEngine;

public class SceneStageSelectorManager : MonoBehaviour {
    /** Buttons */
    public void BTN_Back() {
        uCore.Director.LoadSceneAsync(gameScenes.Menu);
    }
    public void BTN_CompleteGame() {
        uCore.GameManager.ClearGameData();
        uCore.Director.LoadSceneAsync(gameScenes.Credits);
    }
    /** ------- */
}
