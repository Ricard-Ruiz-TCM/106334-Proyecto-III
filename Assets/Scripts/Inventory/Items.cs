using UnityEngine; 

public class Items : MonoBehaviour {

    void Awake() {
        WeaponGladius = Resources.Load<Item>("ScriptableObjects/Items/[Weapon] Gladius");
        Book1 = Resources.Load<Item>("ScriptableObjects/Items/[Book] Debug Knowledge");
    }

    public static Item WeaponGladius;
    public static Item Book1;

}