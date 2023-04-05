using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalizationManager : MonoBehaviour {

    // Estructura del json, montado dentro de una lista de items
    [Serializable]
    private class LocalizationData {
        public LocalizationItem[] items = new LocalizationItem[0];
    }

    // Estrucura del pairKeysValue del json de Localization
    [Serializable]
    private class LocalizationItem {
        public string key = "";
        public string value = "";
    }

    // Observer para el momento de cambiar localización
    public static event Action OnChangeLocalization;

    [SerializeField, Header("Language:")]
    private language _language = language.EN;

    [SerializeField, Header("Localization file Path:")]
    private string _LocalizationPath = "/Resources/Localization/";
    [Header("Localization file format:")]
    private string _format = ".json";

    [SerializeField]
    private Dictionary<string, string> _texts = new Dictionary<string, string>();

    // Unity Awake
    void Awake() {
        ChangeLocalization(_language);
    }

    public void ChangeLocalization(language len) {
        _language = len;
        LoadLocalizationFile(Application.dataPath + _LocalizationPath + _language.ToString() + _format);
        OnChangeLocalization?.Invoke();
    }

    private void LoadLocalizationFile(string path) {
        if (File.Exists(path)) {
            _texts.Clear();
            string json = File.ReadAllText(path);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(json);
            for (int i = 0; i < loadedData.items.Length; i++) {
                _texts.Add(loadedData.items[i].key, loadedData.items[i].value);
            }
        }
    }

    public string GetText(string key) {
        if (!_texts.ContainsKey(key))
            return "X " + key + " X";

        return _texts[key];
    }

}