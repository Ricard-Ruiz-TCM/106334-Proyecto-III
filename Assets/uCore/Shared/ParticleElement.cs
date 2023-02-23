using UnityEngine;

public class ParticleElement : MonoBehaviour {

    public ParticleSystem System { get { return GetComponent<ParticleSystem>(); } }

    public void Play() {
        System.Play();
    }

}
