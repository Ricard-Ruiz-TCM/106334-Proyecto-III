using UnityEngine;

public class EffectsManager : MonoBehaviour {

    [SerializeField, Header("Folder Paths:")]
    private string _EffectsPath = "Effects/";

    /** [SerializeField, Header("Layer:")] WIP
    private string _layerName = "VolumeFX";
    [SerializeField]
    private LayerMask _layer; */

    /** [SerializeField, Header("Canvas Sorting:")] WIP
    private int _sortingOrder = 99;*/

    // Diccionario de Prefabs
    private Container<GameObject> _effects;

    // Unity Awake
    void Awake() {
        //_layer = LayerMask.GetMask(_layerName);
        _effects = new Container<GameObject>(_EffectsPath);
    }

    // * ---------------- *
    // | - Play Effects - |
    // V ---------------- V
    public EffectElement PlayEffect(string file) {
        return PlayEffect(file, null);
    }
    public EffectElement PlayEffect(string file, Vector3 position) {
        return PlayEffect(file).setPosition(position);
    }
    public EffectElement PlayEffect(string file, Transform parent) {
        return IEffect(file).setParent(parent);
    }
    // A ---------------- A

    /** Método IEffect
     * Método encargado de la instanciación del efecto si se crea mediante el uso de prefabs
     * @param string file Dirección del prefab
     * @return EffectElement el objeto creado
     */
    private EffectElement IEffect(string file) {
        return GameObject.Instantiate(_effects.Get(file).gameObject).AddComponent<EffectElement>();
    }

    /* PP Effects WIP
    // * ---------------- *
    // | - Fade Effects - |
    // V ---------------- V
    // Global PP
    public EffectElement FadeIn(float duration, Action callback = null) {
        return iFade(Color.white, Color.black, duration, callback).Set(effects.fadeIn);
    }
    public EffectElement FadeOut(float duration, Action callback = null) {
        return iFade(Color.black, Color.white, duration, callback).Set(effects.fadeOut, duration).destroyAtEnd();
    }
    private EffectElement iFade(Color from, Color to, float duration, Action callback) {
        // Instantiate
        Volume volume = new GameObject("fade").AddComponent<Volume>();
        EffectElement ee = volume.gameObject.AddComponent<EffectElement>();
        ColorAdjustments ca = ee.EffectProfile.Add<ColorAdjustments>();
        volume.gameObject.layer = _layer;

        // Config
        ca.colorFilter.overrideState = true;
        StartCoroutine(C_Fade(ca, from, to, duration, callback));
        return ee;
    }
    private IEnumerator C_Fade(ColorAdjustments colorAdjustments, Color from, Color to, float duration, Action callback) {
        float elapsed = 0f;
        do {
            yield return null;
            colorAdjustments.colorFilter.Interp(from, to, (elapsed / duration));
            elapsed += Time.deltaTime;
        } while (elapsed < duration);
        callback?.Invoke();
    }
    // Canvas
    public EffectElement CanvasFadeIn(float duration, Action callback = null) {
        return iCanvasFade(0f, 1f, duration, callback).Set(effects.fadeIn);
    }
    public EffectElement CanvasFadeOut(float duration, Action callback = null) {
        return iCanvasFade(1f, 0f, duration, callback).Set(effects.fadeOut, duration).destroyAtEnd();
    }
    private EffectElement iCanvasFade(float from, float to, float duration, Action callback) {
        // Instantiate
        Canvas canvas = new GameObject("Canvas").AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.gameObject.AddComponent<CanvasScaler>();
        canvas.sortingOrder = _sortingOrder;

        // Config
        Image img = canvas.gameObject.AddComponent<Image>();
        EffectElement ee = img.gameObject.AddComponent<EffectElement>();
        StartCoroutine(C_CanvasFade(img, from, to, duration, callback));

        return ee;
    }
    private IEnumerator C_CanvasFade(Image img, float from, float to, float duration, Action callback) {
        float elapsed = 0f;
        img.color = Color.black;
        do {
            yield return null;
            Color c = img.color;
            c.a = Mathf.Lerp(from, to, (elapsed / duration));
            img.color = c;
            elapsed += Time.deltaTime;
        } while (elapsed < duration);
        callback?.Invoke();
    }
    // A ---------------- A
    */

    /* Camera Effects WIP
    // * ------------------ *
    // | - Camera Effects - |
    // V ------------------ V
    // Shake
    public EffectElement CameraShake(float duration, float magnitude) {
        return iCameraShake(duration, magnitude).Set(effects.cameraShake, duration).destroyAtEnd();
    }
    private EffectElement iCameraShake(float duration, float magnitude, Action callback = null, GameObject camera = null) {
        // Innitialize
        EffectElement ee = new GameObject("Shake").AddComponent<EffectElement>();
        ee.transform.SetParent(Camera.main.gameObject.transform);
        if (camera)
            ee.Camera = camera;

        // Config
        StartCoroutine(C_Shake(ee.Camera, duration, magnitude));

        return ee;
    }
    private IEnumerator C_Shake(GameObject camera, float duration, float magnitude) {
        float elapsed = 0.0f;
        Vector3 originalPosition = camera.transform.localPosition;
        do {
            yield return null;
            float x = UnityEngine.Random.Range(-1f, 1f) * magnitude;
            float y = UnityEngine.Random.Range(-1f, 1f) * magnitude;
            camera.transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);
            elapsed += Time.deltaTime;
        } while (elapsed < duration);
        camera.transform.localPosition = originalPosition;
    }
    // A ------------------ A*/
}