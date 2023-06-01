using UnityEngine;

public class WeaponHolder : MonoBehaviour {

    private itemID _myWeapon;

    public GameObject _gladius, _hasta, _bow, _pugio, _dolabra, _scutum;

    public void reArm() {
        setWeapon(_myWeapon);
    }

    public void disarm() {
        _gladius.SetActive(false);
        _hasta.SetActive(false);
        _bow.SetActive(false);
        _pugio.SetActive(false);
        _dolabra.SetActive(false);
        _scutum.SetActive(false);
    }

    public void setShield() {
        _scutum.SetActive(true);
    }

    public void setWeapon(itemID weaponID) {
        _myWeapon = weaponID;
        switch (weaponID) {
            case itemID.Gladius:
                _gladius.SetActive(true);
                break;
            case itemID.Hasta:
                _hasta.SetActive(true);
                break;
            case itemID.Bow:
                _bow.SetActive(true);
                break;
            case itemID.Pugio:
                _pugio.SetActive(true);
                break;
            case itemID.Dolabra:
                _dolabra.SetActive(true);
                break;
        }
    }

}