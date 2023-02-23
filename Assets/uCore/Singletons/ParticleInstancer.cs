using System.Collections.Generic;
using UnityEngine;

public class ParticleInstancer : MonoBehaviour {

    private Container<ParticleElement> _elements;

    // Unity Awake
    void Awake() {
        _elements = new Container<ParticleElement>("Particles/");
    }

    // Método para cargar particulas solo 1 vez
    // In: string name -> Nombre ID del prefab
    // In: Transform parent -> Parent de la particulas
    // Out: GameObject -> objeto recien creado
    public ParticleElement PlayParticlesOnce(string name, Transform parent) {
        if (_elements == null) 
            _elements = new Container<ParticleElement>("Particles/");
        return ParticleElement.Instantiate(_elements.Get(name).gameObject, parent).GetComponent<ParticleElement>();
    }

    // Método para reproducir un SFX solo 1 vec
    // In: string name -> Nombre ID del clip
    // In: Vector3 postiion -> Posición del audio en el mundo, 3D NENE, UBICATE
    public ParticleElement PlayParticlesOnce(string name, Vector3 position) {
        ParticleElement p = PlayParticlesOnce(name, uCore.GameManager.ParticleContainer());
        p.transform.position = position;
        return p;
    }

    public ParticleElement PlayLoopedParticles(string name, Transform parent) {
        return PlayParticlesOnce(name, parent);
    }

}