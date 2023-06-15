using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivideSoundtracks : MonoBehaviour
{
    [SerializeField]
    private EventReference combatMusic;

    [SerializeField]
    private StudioEventEmitter eventEmitter;

    private bool hasReproducedCombatMusic;

    void OnEnable()
    {
        TurnManager.onNewRound += SetNextMusic;
        hasReproducedCombatMusic = false;
    }

    public void SetNextMusic(roundType roundType)
    {
        if(roundType == roundType.combat && !hasReproducedCombatMusic)
        {
            eventEmitter.EventReference = combatMusic;
            hasReproducedCombatMusic = true;
        }
    }
}
