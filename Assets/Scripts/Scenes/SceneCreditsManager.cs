using UnityEngine;

public class SceneCreditsManager : MonoBehaviour {

    private void Start() {
        FadeFX.instance.FadeOut();
    }

    /** Buttons */
    public void BTN_Back() {
        uCore.Director.LoadSceneAsync(gameScenes.Menu);
    }
    /** ------- */

}
