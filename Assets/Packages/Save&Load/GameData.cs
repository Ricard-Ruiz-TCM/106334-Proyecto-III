[System.Serializable]
public class GameData : DynamicGameData {
    /** The only purpose of this class is to hold values, create the needed varibales to hold the information you want to store */
    public int deathCount;

    public SerializableDictionary<string, bool> coinsCollected; /** example of a more complex data to store */

    public GameData() {
        deathCount = 0;
        coinsCollected = new SerializableDictionary<string, bool>();
    }
}

