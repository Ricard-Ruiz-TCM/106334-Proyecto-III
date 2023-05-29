using UnityEngine;

public class ShortStatusInfoUI : MonoBehaviour {

    [SerializeField]
    private UIText _txtName;

    public void Set(BuffItem buff) {
        _txtName.Clear();
        if (buff != null) {
            _txtName.SetKey(buff.buff.keyName);
        } else {
            gameObject.SetActive(false);
        }
    }

}
