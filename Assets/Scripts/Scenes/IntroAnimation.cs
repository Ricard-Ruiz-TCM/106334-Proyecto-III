using UnityEngine;

public class IntroAnimation : MonoBehaviour
{

    public void EVENT_IntroAnimationEnds()
    {
        uCore.Director.LoadSceneAsync(gameScenes.Menu, false);
    }
}
