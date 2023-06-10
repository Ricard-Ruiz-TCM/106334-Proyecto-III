using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODManager : MonoBehaviour
{
    public void PlaySound(string path)
    {
        FMOD.Studio.EventInstance playSound;
        playSound = FMODUnity.RuntimeManager.CreateInstance(path);
        playSound.start();
    }
}
