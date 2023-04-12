using UnityEngine;

public class ParticleInstancer : MonoBehaviour {

    // Particles Settings Path
    [SerializeField, Header("Folder Paths:")]
    private string _ParticlesPrefabsPath = "Particles/";

    // Diccionario de ParticleElement
    private Container<GameObject> _prefabs;

    // Unity Awake
    void Awake() {
        _prefabs = new Container<GameObject>(_ParticlesPrefabsPath);
    }

    // * ------------------ *
    // | - Play Particles - |
    // V ------------------ V
    public ParticleElement PlayParticles(string file) {
        return PlayParticles(file, null);
    }
    public ParticleElement PlayParticles(string file, Vector3 position) {
        return PlayParticles(file).setPosition(position);
    }
    public ParticleElement PlayParticles(string file, Transform parent) {
        return IParticles(file).setParent(parent).destroyAtEnd();
    }
    // A ------------------ A

    private ParticleElement IParticles(string file) {
        return GameObject.Instantiate(_prefabs.Get(file).gameObject).AddComponent<ParticleElement>(); ;
    }

}