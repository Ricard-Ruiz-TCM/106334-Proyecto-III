using UnityEngine;

public class AjustesManager : MonoBehaviour
{
    public void ShowSettings()
    {
        this.gameObject.SetActive(true);
    }

    public void HideSettings()
    {
        this.gameObject.GetComponent<MovementStopper>().OnPointerExit(null);
        this.gameObject.SetActive(false);
    }
}
