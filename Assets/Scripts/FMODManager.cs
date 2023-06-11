using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FMODManager : MonoBehaviour
{
    [Range(0,1)]
    public float masterVolume = 1;
    [Range(0, 1)]
    public float musicVolum = 1;
    [Range(0, 1)]
    public float sfxVolum = 1;
    [Range(0, 1)]
    public float ambienceVolum = 1;

    private Transform updatedCameraTransform;
    private Bus masterBus;
    private Bus musicBus;
    private Bus sfxBus;
    private Bus ambienceBus;
    private List<EventInstance> eventInstances;

    public static FMODManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null) 
        {
            Debug.LogError("Found more than one FMOD Manager in the scene");
        }

        instance = this;

        eventInstances = new List<EventInstance>();
        
        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
        ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
    }

    private void Update()
    {
        masterBus.setVolume(masterVolume);
        musicBus.setVolume(musicVolum);
        sfxBus.setVolume(sfxVolum);
        ambienceBus.setVolume(ambienceVolum);
        updatedCameraTransform = Camera.main.transform;
    }

    public void PlayOneShot(EventReference eventReference) 
    {
        RuntimeManager.PlayOneShot(eventReference, updatedCameraTransform.position);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    private void CleanUp()
    {
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }
}
