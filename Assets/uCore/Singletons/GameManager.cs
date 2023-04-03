using UnityEngine;

public class GameManager : MonoBehaviour {

    // Cursor
    [SerializeField, Header("Cursor:")]
    private bool m_CursorLocked = false;
    public bool IsCursorLocked() { return !m_CursorLocked; }

    // M�todo par alternar el lockeo del rat�n en el juego
    public void ToggleCursor() {
        m_CursorLocked = !m_CursorLocked;

        // Lock del cursor
        Cursor.visible = m_CursorLocked;
        Cursor.lockState = (CursorLockMode)(m_CursorLocked ? 1 : 0);
    }

    // M�todo para bloquear el cursor
    public void LockCursor() {
        m_CursorLocked = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // M�todo para desbloquear el cursor
    public void UnlockCursor() {
        m_CursorLocked = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void DisplayCursor() {
        //Cursor.visible = true;
    }

    public void HideCursor() {
        //Cursor.visible = false;
    }

}
