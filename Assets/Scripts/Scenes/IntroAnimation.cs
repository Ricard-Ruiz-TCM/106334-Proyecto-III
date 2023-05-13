using UnityEngine;

public class IntroAnimation : MonoBehaviour {

    /** Animation Event Callback */
    public void EVENT_IntroAnimationEnds() {
        uCore.Director.LoadSceneAsync(gameScenes.Menu, false);
    }

}
