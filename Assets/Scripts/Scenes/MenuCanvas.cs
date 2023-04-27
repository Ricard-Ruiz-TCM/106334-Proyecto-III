using UnityEngine;

public class MenuCanvas : MonoBehaviour
{

    public void BTN_Continue()
    {
        uCore.Director.LoadSceneAsync(gameScenes.StageSelector);
    }

    public void BTN_NewGame()
    {
        Debug.Log("Reset GameData");
        uCore.Director.LoadSceneAsync(gameScenes.StageSelector);
    }

    public void BTN_Settings()
    {
        Debug.Log("Options");
    }

    public void BTN_Credits()
    {
        uCore.Director.LoadSceneAsync(gameScenes.Credits);
    }

    public void BTN_Exit()
    {
        Application.Quit(0);
    }

}
