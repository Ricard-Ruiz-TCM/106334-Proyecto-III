using System;
using UnityEngine;

[Serializable]
public struct StageData {

    [HideInInspector]
    public int ID;
    [Header("Name:")]
    public string keyName;

    [HideInInspector, Header("Stage Type:")]
    public stageType type;
    [HideInInspector]
    public stageTerrain terrain;
    [HideInInspector]
    public stageDifficulty diff;

    [Header("Dialog System?:")]
    public DialogNode innitialDialog;
    public DialogNode tutorialDialog;
    public DialogNode lastDialog;

    [Header("Perks for PerkDialog?:")]
    public Perk[] perks;

    //[Header("Entrance & Exit:")]
    [HideInInspector]
    public stageEntrance exit;
    [HideInInspector]
    public stageEntrance entrance;

    [Header("Objetive:")]
    public stageObjetive objetive;

}
