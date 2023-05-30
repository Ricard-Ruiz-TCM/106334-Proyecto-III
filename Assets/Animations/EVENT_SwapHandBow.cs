using UnityEngine;

public class EVENT_SwapHandBow : MonoBehaviour {

    [SerializeField]
    private Transform _bow;

    [SerializeField]
    private Transform _rHand, _lHand;

    // POSICIONES Y ROTACIONES, NO MIRAR
    #region NO OPEN
    private Vector3 _rRPos = new Vector3(-0.106f, 0.037f, -0.013f);
    private Vector3 _rRRot = new Vector3(0f, -7.813f, 10.937f);
    private Vector3 _rLPos = new Vector3(-0.1142f, 0.0401f, 0.026f);
    private Vector3 _rLRot = new Vector3(182.181f, 9.1210f, 35.936f);
    #endregion

    public void EVENT_2LeftHand() {
        _bow.transform.SetParent(_lHand);
        _bow.transform.localPosition = _rRPos;
        _bow.transform.localEulerAngles = _rRRot;
    }

    public void EVENT_2RightHand() {
        _bow.transform.SetParent(_rHand);
        _bow.transform.localPosition = _rLPos;
        _bow.transform.localEulerAngles = _rLRot;
    }

}
