using UnityEngine;

public class SceneMenuManager : MonoBehaviour {

    private void Awake() {
        FadeFX.instance.FadeOut();
    }

    /** Buttons */
    public void BTN_NewGame() {
        FMODManager.instance.PlayOneShot(FMODEvents.instance.PressButtonUI);
        uCore.Director.LoadSceneAsync(gameScenes.Stage0, false);
        FadeFX.instance.FadeIn(() => { uCore.Director.AllowScene(); });
    }

    public void BTN_Credits() {
        FMODManager.instance.PlayOneShot(FMODEvents.instance.PressButtonUI);
        FadeFX.instance.FadeIn(() => { uCore.Director.LoadSceneAsync(gameScenes.Credits); });
    }

    public void BTN_Exit() {
        FMODManager.instance.PlayOneShot(FMODEvents.instance.PressButtonUI);
        FadeFX.instance.FadeIn(() => { Application.Quit(0); });
    }
    /** ------- */

}
