using UnityEngine;

public class StageSelectorScene : MonoBehaviour {

    public void BTN_NoEvent() {
        uCore.Director.LoadSceneAsync(gameScenes.Stage);
    }

    public void BTN_ComradeEvent() {
        Debug.Log("ComradeEvent");
        uCore.Director.LoadSceneAsync(gameScenes.Event);
    }

    public void BTN_BlacksmithEvent() {
        Debug.Log("BlacksmithEvent");
        uCore.Director.LoadSceneAsync(gameScenes.Event);
    }

    public void BTN_Back() {
        uCore.Director.LoadSceneAsync(gameScenes.Menu);
    }

}
