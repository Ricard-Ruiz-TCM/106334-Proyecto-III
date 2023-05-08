using UnityEngine;

public class SceneCreditsManager : MonoBehaviour {

    /** Buttons */
    public void BTN_Back() {
        uCore.Director.LoadSceneAsync(gameScenes.Menu);
    }
    /** ------- */

}
