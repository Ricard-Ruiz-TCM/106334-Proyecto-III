using UnityEngine;
using UnityEngine.UI;

public class MusicSliderScript : MonoBehaviour
{
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    public void ChangeMasterVolume()
    {
        FMODManager.instance.ChangeMasterVolume(slider.value);
    }

    public void ChangeMusicVolume()
    {
        FMODManager.instance.ChangeMusicVolume(slider.value);
    }

    public void ChangeAmbienceVolume()
    {
        FMODManager.instance.ChangeAmbienceVolume(slider.value);
    }

    public void ChangeSFXVolume()
    {
        FMODManager.instance.ChangeSFXVolume(slider.value);
    }
}
