using System;
using UnityEngine;

[Serializable]
public struct StageData {

    [Header("Stage ID & Name:")]
    public int ID;
    public string keyName;

    [Header("Stage Type:")]
    public stageType type;
    public stageTerrain terrain;
    public stageDifficulty diff;

    [Header("Dialog System?:")]
    public DialogNode innitialDialog;

    [Header("Entrance & Exit:")]
    public stageEntrance exit;
    public stageEntrance entrance;

    [Header("Objetive:")]
    public stageObjetive objetive;

}
