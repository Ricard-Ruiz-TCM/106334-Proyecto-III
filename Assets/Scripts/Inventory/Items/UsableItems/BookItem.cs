using UnityEngine;

[CreateAssetMenu(fileName = "new BookItem", menuName = "Items/Book Item")]
public class BookItem : UsableItem {

    public override void Use() {
        Debug.Log("BookItem USE");
    }

}
