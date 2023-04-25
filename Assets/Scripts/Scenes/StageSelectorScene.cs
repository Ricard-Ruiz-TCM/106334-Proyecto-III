using UnityEngine;

public class StageSelectorScene : MonoBehaviour {

    public void BTN_NoEvent() {
        uCore.GameManager.RoadEvent = roadEvent.noEvent;
        uCore.Director.LoadSceneAsync(gameScenes.Stage);
    }

    public void BTN_ComradeEvent() {
        uCore.GameManager.RoadEvent = roadEvent.comrade;
        uCore.Director.LoadSceneAsync(gameScenes.Event);
    }

    public void BTN_BlacksmithEvent() {
        uCore.GameManager.RoadEvent = roadEvent.blacksmith;
        uCore.Director.LoadSceneAsync(gameScenes.Event);
    }

    public void BTN_Back() {
        uCore.Director.LoadSceneAsync(gameScenes.Menu);
    }

}
