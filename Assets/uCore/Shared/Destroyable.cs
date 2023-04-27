using System.Collections;
using UnityEngine;

/** class Destroyable
 * ------------------
 * 
 * Comprotamiento para destuir un objeto utilizando Coroutines
 * 
 * @author: Nosink Ð (Ricard Ruiz)
 * @version: v1.0 (04/2023)
 * 
 */

public class Destroyable : MonoBehaviour {

    /** Destroy Time */
    private float _destroyTime;

    // Unity Start
    void Start() {
        StartCoroutine(C_Destory());
    }

    /** Método destroyIn
     * Establece el tiempo para la destucción
     * @param float time Duración de vida */
    public void destroyIn(float time) {
        _destroyTime = time;
    }

    /** Método C_Destroy
     * Coroutine que dsetruye el objeto */
    private IEnumerator C_Destory() {
        yield return new WaitForSeconds(_destroyTime);
        GameObject.Destroy(this.gameObject);
    }

}
