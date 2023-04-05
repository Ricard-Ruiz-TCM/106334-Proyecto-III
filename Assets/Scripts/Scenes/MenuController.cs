using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public void BTN_Play() {
        uCore.Director.LoadSceneAsync(gameScenes.Game);
    }

    public void BTN_Settings() {

    }

    public void BTN_Exit() {
        Application.Quit(0);
    }

    public void BTN_Album() {

    }

}
