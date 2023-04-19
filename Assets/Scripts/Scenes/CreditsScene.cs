using UnityEngine;

public class CreditsScene : MonoBehaviour {

    public void BTN_Back() {
        uCore.Director.LoadSceneAsync(gameScenes.Menu);
    }

}
