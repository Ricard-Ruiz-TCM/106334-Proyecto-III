using UnityEngine;
using UnityEngine.UI;

public class DiegHealthBarUI : MonoBehaviour {

    public Actor me;
    public Image bar;

    // Update is called once per frame
    void Update() {
        transform.LookAt(Camera.main.transform);
        bar.fillAmount = ((float)me.GetHealth() / (float)me.MaxHealth());
    }

}
