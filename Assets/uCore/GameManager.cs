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

    #region Progress

    public int StageID = 0;
    public StageData NextStage;
    public StageData LastStage;
    public List<StageData> StageRecord = new List<StageData>();

    /** Método de selección de stage */
    public void StageSelected(StageData data) {
        StageID = data.ID;
        LastStage = NextStage;
        NextStage = data;
        if (!StageRecord.Contains(data)) {
            StageRecord.Add(data);
        }
    }

    #endregion

    #region Dialogues

    private DialogNode _comradeNode;
    public DialogNode ComradeNode {
        get {
            if (_comradeNode == null)
                _comradeNode = Resources.Load<DialogNode>("ScriptableObjects/Dialogue/Comrade/[C, 0] Intro");

            return _comradeNode;
        }
        set {
            _comradeNode = value;
        }
    }

    private DialogNode _blacksmithNode;
    public DialogNode BlacksmithNode {
        get {
            if (_blacksmithNode == null)
                _blacksmithNode = Resources.Load<DialogNode>("ScriptableObjects/Dialogue/Blacksmith/[B, 0] Intro");

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

    #region Skills

    private Dictionary<skills, Skill> _skills;

    public void LoadSkillData() {
        _skills = new Dictionary<skills, Skill>();
        Skill[] allSkills = Resources.LoadAll<Skill>("ScriptableObjects/Skills");
        foreach (Skill sk in allSkills) {
            skills skl;
            if (Enum.TryParse(sk.name, out skl)) {
                _skills.Add(skl, sk);
            } else {
                Debug.LogWarning("Missmatch de nombre con el SO y el Enum en: " + sk.name);
            }
        }
    }

    public Skill GetSkill(skills name) {
        if (_skills == null) {
            LoadSkillData();
        }
        return _skills[name];
    }

    #endregion

    #region Perks

    private Dictionary<perks, Perk> _perks;

    public void LoadPerkData() {
        _perks = new Dictionary<perks, Perk>();
        Perk[] allPerks = Resources.LoadAll<Perk>("ScriptableObjects/Perks");
        foreach (Perk perk in allPerks) {
            perks skl;
            if (Enum.TryParse(perk.name, out skl)) {
                _perks.Add(skl, perk);
            } else {
                Debug.LogWarning("Missmatch de nombre con el SO y el Enum en: " + perk.name);
            }
        }
    }

    public Perk GetPerk(perks name) {
        if (_perks == null) {
            LoadPerkData();
        }
        return _perks[name];
    }

    #endregion

    #region Status

    private Dictionary<buffsnDebuffs, Status> _status;

    public void LoadStatusData() {
        _status = new Dictionary<buffsnDebuffs, Status>();
        Status[] allStatus = Resources.LoadAll<Status>("ScriptableObjects/Status");
        foreach (Status astatus in allStatus) {
            buffsnDebuffs sts;
            if (Enum.TryParse(astatus.name, out sts)) {
                _status.Add(sts, astatus);
            } else {
                Debug.LogWarning("Missmatch de nombre con el SO y el Enum en: " + astatus.name);
            }
        }
    }

    public Status GetStatus(buffsnDebuffs name) {
        if (_status == null) {
            LoadStatusData();
        }
        return _status[name];
    }

    #endregion

    #region Player

    public Player Player;

    public void RestorePlayer(Player player) {
        Player = player;
    }

    public void SavePlayer(Player player) {
        Player = null;
    }

    #endregion

    #region GameData

    public bool ExistGameData() {
        // TODO
        Debug.Log("CHECKING SI EXISTE GAME DATA, TODO");
        return false;
    }

    /** LOAD */
    public void LoadGameData() {
        // TODO
        Debug.Log("CARGANDO GAME DATA, TODO");
    }

    /** SAVE */
    public void SaveGameData() {
        // TODO
        Debug.Log("GUARDANDO GAME DATA, TODO");
    }

    /** CLEAR */
    public void ClearGameData() {
        // TODO
        Debug.Log("CLEAR GAME DATA, TODO");
    }

    #endregion

}
