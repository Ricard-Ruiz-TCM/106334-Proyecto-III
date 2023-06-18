using UnityEngine;

public class SceneOptionsManager : MonoBehaviour
{
    private void Start()
    {
        FadeFX.instance.FadeOut();
    }

    /** Buttons */
    public void BTN_Back()
    {
        uCore.Director.LoadSceneAsync(gameScenes.Menu);
    }
    /** ------- */
}
