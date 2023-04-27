using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{

    // Estructura del json, montado dentro de una lista de items
    [Serializable]
    private class LocalizationData
    {
        public LocalizationItem[] items = new LocalizationItem[0];
    }

    // Estrucura del LocalizationItem del json de Localization
    [Serializable]
    private class LocalizationItem
    {
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
    void Awake()
    {
        ChangeLocalization(_language);
    }

    /** Método ChangeLocalization
     * Cambia el idioma del proyecto y invoca los callbacks
     * @paam language len Nuevo idioma */
    public void ChangeLocalization(language len)
    {
        _language = len;
        LoadLocalizationFile(Application.dataPath + _LocalizationPath + _language.ToString() + _format);
        OnChangeLocalization?.Invoke();
    }

    /** Método LoadLocalizationFile
     * Carga el fichero de idioma al diccionario
     * @param string path Dirección del fichero */
    private void LoadLocalizationFile(string path)
    {
        if (File.Exists(path))
        {
            _texts.Clear();
            string json = File.ReadAllText(path);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(json);
            for (int i = 0; i < loadedData.items.Length; i++)
            {
                _texts.Add(loadedData.items[i].key, loadedData.items[i].value);
            }
        }
    }

    /** Método GetText
     * Recupera una key de texto
     * @param string key Key ID del texto dentro del json
     * @return string Texto del idioma especifico */
    public string GetText(string key)
    {
        if (!Exists(key))
            return "NO KEY";

        return _texts[key];
    }

    /** Método Exists
     * Compruba si existe una key en el sistema
     * @return bool true -> existe | false -> no existe */
    public bool Exists(string key)
    {
        return _texts.ContainsKey(key);
    }

}
