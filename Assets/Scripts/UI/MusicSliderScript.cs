using UnityEngine;
using UnityEngine.UI;

public class MusicSliderScript : MonoBehaviour
{
    private Slider slider;
    private enum sliderType { Master, Music, Ambience, SFX }

    [SerializeField] private sliderType sliderT;

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        switch(sliderT)
        {
            case sliderType.Master:
                slider.value = FMODManager.instance.masterVolume;
                break;
            case sliderType.Music:
                slider.value = FMODManager.instance.musicVolum;
                break;
            case sliderType.Ambience:
                slider.value = FMODManager.instance.ambienceVolum;
                break;
            case sliderType.SFX:
                slider.value = FMODManager.instance.sfxVolum;
                break;
            default:
                break;
        }
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
