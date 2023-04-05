using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour {

    public static event Action OnSceneLoaded;
    private AsyncOperation _asyncOp;

    public void LoadScene(gameScenes scene) {
        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadSceneAsync(gameScenes scene, bool allow = true) {
        StartCoroutine(C_LoadSceneAsync(scene, allow));
        LoadScene(gameScenes.LoadingScene);
    }

    private IEnumerator C_LoadSceneAsync(gameScenes scene, bool allow) {
        yield return null;
        _asyncOp = SceneManager.LoadSceneAsync(scene.ToString());
        _asyncOp.allowSceneActivation = allow;
        if (_asyncOp.allowSceneActivation) {
            while (!_asyncOp.isDone) { yield return null; }
        } else {
            while (_asyncOp.progress < 0.9f) { yield return null; } 
        }
        OnSceneLoaded?.Invoke();
    }

    public void AllowScene() {
        StartCoroutine(C_AllowScene());
    }

    private IEnumerator C_AllowScene() {
        while (_asyncOp == null) { yield return null; }
        if (_asyncOp != null) _asyncOp.allowSceneActivation = true;
    }

    public bool LoadingDone() {
        return (_asyncOp != null ? _asyncOp.isDone : false);
    }

    public float LoadingProgress() {
        return (_asyncOp != null ? _asyncOp.progress : 0f);
    }

}
