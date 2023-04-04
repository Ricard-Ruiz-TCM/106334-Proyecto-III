using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroAnimation : MonoBehaviour {

    public void EVENT_IntroAnimationEnds() {
        Debug.Log("TIME");
        SceneManager.LoadScene("LoadingScene");
    }
}
