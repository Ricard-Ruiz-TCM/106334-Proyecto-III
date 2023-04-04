using System.Collections;
using UnityEngine;

public class Destroyable : MonoBehaviour {

    [SerializeField, Header("Time")]
    private float _destroyTime;

    // Unity Start
    void Start() {
        StartCoroutine(DestroyOnTime());
    }

    // Sete el tiempo
    public void destroyIn(float time) {
        _destroyTime = time;
    }

    // Método para Coroutine
    private IEnumerator DestroyOnTime() {
        yield return new WaitForSeconds(_destroyTime);
        GameObject.Destroy(this.gameObject);
    }


}
