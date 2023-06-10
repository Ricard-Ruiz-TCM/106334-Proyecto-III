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
    public float vfxVolum = 1;

    [SerializeField] private Transform cameraTransform;

    private Bus masterBus;
    private Bus musicBus;
    private Bus vfxBus;
    private List<EventInstance> eventInstances;

    public static FMODManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null) { }

        instance = this;

        eventInstances = new List<EventInstance>();
        
        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        vfxBus = RuntimeManager.GetBus("bus:/VFX");
    }

    private void Update()
    {
        masterBus.setVolume(masterVolume);
        musicBus.setVolume(musicVolum);
        vfxBus.setVolume(vfxVolum);
    }

    public void PlayOneShot(EventReference eventReference) 
    {
        RuntimeManager.PlayOneShot(eventReference, cameraTransform.position);
    }

    public EventInstance CreateInstance(EventReference eventReference)
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
