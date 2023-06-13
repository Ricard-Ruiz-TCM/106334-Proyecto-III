using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeFX : MonoBehaviour {

    private static FadeFX _instance;
    public static FadeFX instance {
        get {
            if (_instance == null)
                _instance = FindObjectOfType<FadeFX>();
            return _instance;
        }

    }

    public Image img;
    [Range(0f, 1f)]
    public float alpha;
    public float duration;

    public bool fading = false;

    void Awake() {
        _instance = this;
    }

    public void FadeIn(Action callback = null) {
        if (fading)
            return;

        alpha = 0f;
        fading = true;
        StartCoroutine(CFade(1f, callback));
    }

    public void FadeOut(Action callback = null) {
        if (fading)
            return;

        alpha = 1f;
        fading = true;
        StartCoroutine(CFade(-1f, callback));
    }

    public IEnumerator CFade(float target, Action callback = null) {

        img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);

        float speed = target / duration;

        while (!Mathf.Approximately(img.color.a, Mathf.Clamp(target, 0f, 1f))) {
            alpha += speed * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);
            img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
            yield return null;
        }

        fading = false;
        callback?.Invoke();
    }

}
