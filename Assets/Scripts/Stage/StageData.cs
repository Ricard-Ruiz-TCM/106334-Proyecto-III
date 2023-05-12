using System;

[Serializable]
public struct StageData {

    public int ID;
    public string keyName;

    public stageType type;
    public stageTerrain terrain;
    public stageDifficulty diff;

    public DialogNode innitialDialog;

    public stageEntrance entrance;
    public stageObjetive objetive;


}
