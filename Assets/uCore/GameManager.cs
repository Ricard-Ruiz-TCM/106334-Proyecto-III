using System;
using System.Collections.Generic;
using UnityEngine;

/** class GameManager
 * ------------------
 * 
 * Clase de libre uso para el proyecto
 * Se puede hacer y deshacer en ella cualquier cosa que le juego requiera
 * Acceso mediante uCore
 * 
 * @see uCore
 * 
 * @author: Nosink Ð (Ricard Ruiz)
 * @version: v2.0 (04/2023)
 * 
 */

public class GameManager : MonoBehaviour {

    // Evento que nos ofrece el road entre stages
    public roadEvent RoadEvent;

    #region Dialogues

    private DialogueNode _comradeNode;
    public DialogueNode ComradeNode {
        get {
            if (_comradeNode == null)
                _comradeNode = Resources.Load<DialogueNode>("ScriptableObjects/Dialogue/Comrade/[C, 0] Intro");

            return _comradeNode;
        }
        set {
            _comradeNode = value;
        }
    }

    private DialogueNode _blacksmithNode;
    public DialogueNode BlacksmithNode {
        get {
            if (_blacksmithNode == null)
                _blacksmithNode = Resources.Load<DialogueNode>("ScriptableObjects/Dialogue/Blacksmith/[B, 0] Intro");

            return _blacksmithNode;
        }
        set {
            _blacksmithNode = value;
        }
    }

    #endregion

    #region Items

    private Dictionary<items, Item> _items;

    public void LoadItemData() {
        _items = new Dictionary<items, Item>();
        Item[] allItems = Resources.LoadAll<Item>("ScriptableObjects/Items");
        foreach (Item it in allItems) {
            items itE;
            if (Enum.TryParse(it.name, out itE)) {
                _items.Add(itE, it);
            } else {
                Debug.LogWarning("Missmatch de nombre con el SO y el Enum en: " + it.name);
            }
        }
    }

    public Item GetItem(items name) {
        if (_items == null) {
            LoadItemData();
        }
        return _items[name];
    }

    #endregion

}
