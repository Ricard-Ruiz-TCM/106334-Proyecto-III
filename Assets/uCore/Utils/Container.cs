using System.Collections.Generic;
using UnityEngine;

/** abstract class Container<T>
 * --------------------------
 * 
 * Contenedor de todo tipo de elementos T siempre que tengan algo qeu ver con Object
 * Carga elementos como prefabs, scriptableObjects, etcs.
 *
 * @author: Nosink Ð (Ricard Ruiz)
 * @version: v2.2 (04/2023)
 * 
 */

[System.Serializable]
public class Container<T> : Object where T : Object {

    public string Path {
        get; private set;
    }
    public List<T> Elements {
        get; private set;
    }
    public Dictionary<string, int> Diccionary {
        get; private set;
    }

    /** Constructor
     * @param string path Ubicación */
    public Container(string path = "NONE") {
        Path = path;
        Elements = new List<T>();
        Diccionary = new Dictionary<string, int>();
    }

    /** Método Get & Load
     * Busca y/o Carga un elementos nuevo
     * @param string name Ubicación/Nombre del archivo
     * @return T Elemento almacenado */
    public T Get(string name) {
        if (!Exists(name)) {
            ILoad(name);
        }
        return Elements[Diccionary[name]];
    }
    public T Load(string name) {
        return Get(name);
    }

    /** Método TryGet
     * Si existe un elemento, lo saca por OUT
     * @param T elemento encontrado
     * @return bool True -> Existe | False -> No Existe */
    public bool TryGet(string name, out T value) {
        if (Exists(name))
            value = Get(name);
        else
            value = null;
        return (value != null);
    }

    /** Método Add
     * Carga un objeto ya creado por código al contenedor
     * @param string name Nombre ID para el diccionario
     * @param T t Elemento ya creado
     * @return T el elemento creado */
    public T Add(string name, T t) {
        int i = FindSpot();
        if (i == -1) {
            i = Elements.Count;
            Elements.Add(t);
        } else {
            Elements[i] = t;
        }
        Diccionary.Add(name, i);
        return Elements[Diccionary[name]];
    }

    /** Método Remove
     * @param string name Elemento a eliminar */
    public void Remove(string name) {
        if (!Exists(name))
            return;
        Elements[Diccionary[name]] = null;
        Diccionary.Remove(name);
    }

    /** Método Clear */
    public void Clear() {
        Elements.Clear();
        Diccionary.Clear();
    }

    /** Método Exists
     * @param string name Elemento a comprobar */
    public bool Exists(string name) {
        return Diccionary.ContainsKey(name);
    }

    /** Método Load
     * @param string name Elemento a cargar */
    private void ILoad(string name) {
        if (Path == "NONE")
            return;
        int i = FindSpot();
        if (i == -1) {
            i = Elements.Count;
            Elements.Add(Resources.Load<T>(Path + name));
        } else {
            Elements[i] = Resources.Load<T>(Path + name);
        }
        Diccionary.Add(name, i);
    }

    /** Método FindSpot
     * @return int i Valor con un elemento Null o -1 */
    private int FindSpot() {
        int i = -1;
        for (int a = 0; a < Elements.Count; a++) {
            if (i != -1)
                break;
            if (Elements[a] == null)
                i = a;
        }
        return i;
    }

}
