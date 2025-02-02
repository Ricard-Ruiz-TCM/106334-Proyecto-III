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
 * @author: Nosink � (Ricard Ruiz)
 * @version: v2.0 (04/2023)
 * 
 */

public class GameManager : MonoBehaviour {

    #region Progress

    public int StageID = 0;
    public StageData NextStage;
    public StageData LastStage;
    public List<StageData> StageRecord = new List<StageData>();

    /** M�todo de selecci�n de stage */
    public void StageSelected(StageData data) {
        StageID = data.ID;
        LastStage = NextStage;
        NextStage = data;
        if (!StageRecord.Contains(data)) {
            StageRecord.Add(data);
        }
    }

    #endregion

    #region Items

    private Dictionary<itemID, Item> _items;

    public void LoadItemData() {
        _items = new Dictionary<itemID, Item>();
        Item[] allItems = Resources.LoadAll<Item>("ScriptableObjects/Items");
        foreach (Item it in allItems) {
            itemID itE;
            if (Enum.TryParse(it.name, out itE)) {
                _items.Add(itE, it);
            } else {
                Debug.LogWarning("Missmatch de nombre con el SO y el Enum en: " + it.name);
            }
        }
    }

    public Item GetItem(itemID name) {
        if (_items == null) {
            LoadItemData();
        }
        return _items[name];
    }

    #endregion

    #region Skills

    private Dictionary<skillID, Skill> _skills;

    public void LoadSkillData() {
        _skills = new Dictionary<skillID, Skill>();
        Skill[] allSkills = Resources.LoadAll<Skill>("ScriptableObjects/Skills");
        foreach (Skill sk in allSkills) {
            skillID skl;
            if (Enum.TryParse(sk.name, out skl)) {
                _skills.Add(skl, sk);
            } else {
                Debug.LogWarning("Missmatch de nombre con el SO y el Enum en: " + sk.name);
            }
        }
    }

    public Skill GetSkill(skillID name) {
        if (_skills == null) {
            LoadSkillData();
        }
        return _skills[name];
    }

    #endregion

    #region Perks

    private Dictionary<perkID, Perk> _perks;

    public void LoadPerkData() {
        _perks = new Dictionary<perkID, Perk>();
        Perk[] allPerks = Resources.LoadAll<Perk>("ScriptableObjects/Perks");
        foreach (Perk perk in allPerks) {
            perkID skl;
            if (Enum.TryParse(perk.name, out skl)) {
                _perks.Add(skl, perk);
            } else {
                Debug.LogWarning("Missmatch de nombre con el SO y el Enum en: " + perk.name);
            }
        }
    }

    public Perk GetPerk(perkID name) {
        if (_perks == null) {
            LoadPerkData();
        }
        return _perks[name];
    }

    #endregion

    #region Buffs

    private Dictionary<buffsID, Buff> _status;

    public void LoadBuffData() {
        _status = new Dictionary<buffsID, Buff>();
        Buff[] allStatus = Resources.LoadAll<Buff>("ScriptableObjects/Buffs");
        foreach (Buff buff in allStatus) {
            buffsID buffID;
            if (Enum.TryParse(buff.name, out buffID)) {
                _status.Add(buffID, buff);
            } else {
                Debug.LogWarning("Missmatch de nombre con el SO y el Enum en: " + buff.name);
            }
        }
    }

    public Buff GetBuff(buffsID name) {
        if (_status == null) {
            LoadBuffData();
        }
        return _status[name];
    }

    #endregion

    #region Player


    [SerializeField, Header("Player Data:")]
    private ArmorInventoryItem _armor;
    [SerializeField]
    private ShieldInventoryItem _shield;

    [SerializeField]
    private List<perkID> _playerPerks = new List<perkID>();
    private List<skillID> _playerSkills = new List<skillID>();

    private Actor _player;

    public void setPlayer(Actor player) {
        _player = player;
    }

    public Actor getPlayer() {
        return _player;
    }

    public List<perkID> restorePerks() {
        return _playerPerks;
    }

    public List<skillID> restoreSkills() {
        return _playerSkills;
    }

    public void savePerks(Actor player) {
        _playerPerks.Clear();
        foreach (PerkItem pkI in player.perks.perks) {
            _playerPerks.Add(pkI.perk.ID);
        }
    }

    public void saveSkills(Actor player) {
        _playerSkills.Clear();
        foreach (SkillItem skI in player.skills.skills) {
            _playerSkills.Add(skI.skill.ID);
        }
    }

    public bool haveWeaponSaved() {
        return weaponAvaliable;
    }

    public WeaponInventoryItem getWeaponInv() {
        return new WeaponInventoryItem() { upgrade = upgradeWeaponLvl, weapon = _weapon };
    }


    bool weaponAvaliable = false;
    public int upgradeWeaponLvl;
    public WeaponItem _weapon;

    public void saveWeapon(WeaponInventoryItem wp) {
        weaponAvaliable = true;
        _weapon = wp.weapon;
        upgradeWeaponLvl = wp.upgrade;
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
