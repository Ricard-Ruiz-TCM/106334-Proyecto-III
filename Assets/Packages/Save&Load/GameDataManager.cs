using System.Collections.Generic;
using UnityEngine;

public static class GameDataManager {


    private static GameData gameData;

    private static List<ISaveData> gameDataHolders;

    private static FileDataHandler dataHandler;




    static GameDataManager() {

        DataConfig config = (DataConfig)Resources.Load("DataConfig");
        gameDataHolders = new List<ISaveData>();
        dataHandler = new FileDataHandler(Application.persistentDataPath, config.GetFileName(), config.GetEncrypt());
        Application.quitting += SaveGame;
        LoadGame();



    }
    public static void NewGame() {
        gameData = new GameData();
    }

    private static void LoadGame() {
        /** Load any data saved in the Data Handler */

        gameData = dataHandler.Load();


        if (gameData == null) {
            Debug.LogError("No game data was found. A new game is created");
            NewGame();
        }

        /** We send the data to load to any script who need it */


    }


    private static void SaveGame() {


        if (gameData == null) {
            Debug.LogError("No game data was found. Can't save the game");
            return;
        }

        foreach (ISaveData dataHolder in gameDataHolders) {
            dataHolder.SaveData();
        }

        /** We save the data into the data handler */

        dataHandler.Save(gameData);
    }


    public static T Load<T>(string name) {
        Debug.Log(gameData);
        return gameData.Get<T>(name);
    }
    public static void Save(string name, object value) {
        gameData.Put(name, value);
    }



    public static void Add(ISaveData dataHolder) {
        gameDataHolders.Add(dataHolder);
    }

    public static void Remove(ISaveData dataHolder) {
        gameDataHolders.Remove(dataHolder);
    }

}
