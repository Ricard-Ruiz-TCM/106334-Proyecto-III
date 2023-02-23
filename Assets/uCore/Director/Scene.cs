using UnityEngine;

/** abstract class uCScene
 * --------------------------
 * 
 * Estructura para gestionar la escena
 * 
 * @author: Nosink Ð (Ricard Ruiz)
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

    // Método para recuperar el StartPoint del nivel
    // Out: Vector3 -> posición dentro de la jerarquía
    public Vector3 StartPoint() {
        return m_StartPoint.position;
    }


}
