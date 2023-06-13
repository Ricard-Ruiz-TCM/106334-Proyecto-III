using UnityEngine;

public class SceneMenuManager : MonoBehaviour {

    private void Awake() {
        FadeFX.instance.FadeOut();
    }

    /** Buttons */
    public void BTN_NewGame() {
        FadeFX.instance.FadeIn(() => { uCore.Director.LoadSceneAsync(gameScenes.Stage0); });
    }

    public void BTN_Credits() {
        FadeFX.instance.FadeIn(() => { uCore.Director.LoadSceneAsync(gameScenes.Credits); });
    }

    public void BTN_Exit() {
        FadeFX.instance.FadeIn(() => { Application.Quit(0); });
    }
    /** ------- */

}
