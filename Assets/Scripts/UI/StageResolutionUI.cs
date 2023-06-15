using System;
using UnityEngine;

public class StageResolutionUI : MonoBehaviour {

    [SerializeField]
    public UIText _txtResolution;
    [SerializeField]
    public GameObject _btnVictory;
    [SerializeField]
    public GameObject _btnDefeat;

    private Action _success;
    private Action _failure;

    /** Establece la informaicón básica del stage, nombre y objetivo */
    public void SetResolution(stageResolution res, Action success, Action failure) {
        _txtResolution.SetKey(res.ToString());
        _success = success;
        _failure = failure;
        if (res.Equals(stageResolution.defeat)) {
            FMODManager.instance.PlayOneShot(FMODEvents.instance.GameOver);
            _btnVictory.SetActive(false);
        } else {
            FMODManager.instance.PlayOneShot(FMODEvents.instance.Victory);
            _btnDefeat.SetActive(false);
        }
    }

    public void BTN_Defeat() {
        _failure();
    }

    public void BTN_Victory() {
        _success();
    }

}

