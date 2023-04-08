using UnityEngine;

public abstract class BasicElement<T> : MonoBehaviour where T : MonoBehaviour {

    public T destroyOnTime(float time) {
        this.gameObject.AddComponent<Destroyable>().destroyIn(time);
        return this as T;
    }

    public T persistent() {
        Destroyable ds = this.gameObject.GetComponent<Destroyable>();
        if (ds != null) { GameObject.Destroy(ds); }
        return this as T;
    }

    public T setParent(Transform parent) {
        this.transform.SetParent(parent);
        return this as T;
    }

    public T setPosition(Vector3 position) {
        this.transform.position = position;
        return this as T;
    }

    public abstract T destroyoAtEnd();
    
}
