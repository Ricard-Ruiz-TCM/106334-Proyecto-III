using UnityEngine;

/** abstract class uCScene
 * --------------------------
 * 
 * Estructura para gestionar la escena
 * 
 * @author: Nosink � (Ricard Ruiz)
 * @version: v1.0 (12/2022)
 * 
 */
public abstract class Scene : MonoBehaviour {

    [SerializeField, Header("Scene Name:")]
    private string _name;
    public string Name() { return _name; }
    public void SetSceneName(string name) { _name = name; }

    [SerializeField, Header("StartPoint:")]
    private Transform m_StartPoint;

    // M�todo para recuperar el StartPoint del nivel
    // Out: Vector3 -> posici�n dentro de la jerarqu�a
    public Vector3 StartPoint() {
        return m_StartPoint.position;
    }


}
