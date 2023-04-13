using UnityEngine;

/** abstract class BasicElement
 * ----------------------------
 * 
 * Estructura "base" para la gestión de los "elements"
 * Una forma de hacer instanciación y control de Audios, Particulas y otros
 * 
 * @author: Nosink Ð (Ricard Ruiz)
 * @version: v1.0 (04/2023)
 * 
 */

public abstract class BasicElement<T> : MonoBehaviour where T : MonoBehaviour {

    /** Método destroyOnTime
     * Agrega el comportamiento "Destroyable" por un tiempo fijo
     * @param float time Tiempo para la destrucción
     * @return T tipo del objeto BasicElement<T> */
    public T destroyOnTime(float time) {
        this.gameObject.AddComponent<Destroyable>().destroyIn(time);
        return this as T;
    }

    /** Método persistent
     * Destruye, si existe, el componente "DEstroyable"
     * @return T tipo del objeto BasicElement<T> */
    public T persistent() {
        Destroyable ds = this.gameObject.GetComponent<Destroyable>();
        if (ds != null) {
            GameObject.Destroy(ds);
        }
        return this as T;
    }

    /** Método setParent
     * Asigna el parent al objeto
     * @param Transform parent Trasnform del futuro padre 
     * @return T tipo del objeto BasicElement<T> */
    public T setParent(Transform parent) {
        this.transform.SetParent(parent);
        return this as T;
    }

    /** Método setPosition
     * Asigna la posición del objeto
     * @param Vector3 position Nueva posición
     * @return T tipo del objeto BasicElement<T> */
    public T setPosition(Vector3 position) {
        this.transform.position = position;
        return this as T;
    }

    /** Método abstracto destroyAtEnd */
    public abstract T destroyAtEnd();

}
