using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour {
    [field: Header("SFX")]
    [field: SerializeField]
    public EventReference MissAttack {
        get; private set;
    }
    [field: SerializeField]
    public EventReference InicioLanzarFlecha {
        get; private set;
    }
    [field: SerializeField]
    public EventReference FlechaContraCarne {
        get; private set;
    }
    [field: SerializeField]
    public EventReference DolabraContraCarne {
        get; private set;
    }
    [field: SerializeField]
    public EventReference GladiusContraCarne {
        get; private set;
    }
    [field: SerializeField]
    public EventReference HastaContraCarne {
        get; private set;
    }
    [field: SerializeField]
    public EventReference PugioContraCarne {
        get; private set;
    }
    [field: SerializeField]
    public EventReference FlechaPiedra {
        get; private set;
    }
    [field: SerializeField]
    public EventReference DolabraPiedra {
        get; private set;
    }
    [field: SerializeField]
    public EventReference GladiusPiedra {
        get; private set;
    }
    [field: SerializeField]
    public EventReference HastaPiedra {
        get; private set;
    }
    [field: SerializeField]
    public EventReference PugioPiedra {
        get; private set;
    }
    [field: SerializeField]
    public EventReference LluviaDeFlechas {
        get; private set;
    }
    [field: SerializeField]
    public EventReference Bloodlust {
        get; private set;
    }
    [field: SerializeField]
    public EventReference UsoHabilidad {
        get; private set;
    }
    [field: SerializeField]
    public EventReference Cleave {
        get; private set;
    }
    [field: SerializeField]
    public EventReference Stun {
        get; private set;
    }
    [field: SerializeField]
    public EventReference Disarm {
        get; private set;
    }
    [field: SerializeField]
    public EventReference DoubleLungue {
        get; private set;
    }
    [field: SerializeField]
    public EventReference DoubleMiss {
        get; private set;
    }
    [field: SerializeField]
    public EventReference ImperialCry {
        get; private set;
    }
    [field: SerializeField]
    public EventReference TrojanHorse {
        get; private set;
    }
    [field: SerializeField]
    public EventReference Vanish {
        get; private set;
    }
    [field: SerializeField]
    public EventReference Steps {
        get; private set;
    }
    [field: SerializeField]
    public EventReference PlayerDeath {
        get; private set;
    }
    [field: SerializeField]
    public EventReference GameOver {
        get; private set;
    }
    [field: SerializeField]
    public EventReference SoldierDeath {
        get; private set;
    }
    [field: SerializeField]
    public EventReference ConfirmPerk {
        get; private set;
    }
    [field: SerializeField]
    public EventReference PressButtonUI {
        get; private set;
    }
    [field: SerializeField]
    public EventReference PressButtonSpeed
    {
        get; private set;
    }
    [field: SerializeField]
    public EventReference TextSound
    {
        get; private set;
    }
    [field: SerializeField]
    public EventReference Victory
    {
        get; private set;
    }

    public static FMODEvents instance {
        get; private set;
    }

    private void Awake() {
        if (instance != null)
        {
            //MORE THAN ONE EVENT
        }
        else
            instance = this;
    }


}
