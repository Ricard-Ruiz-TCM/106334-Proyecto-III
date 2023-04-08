using UnityEngine;

public class ParticleElement : BasicElement<ParticleElement> {

    public ParticleSystem System { get { return GetComponent<ParticleSystem>(); } }

    public ParticleElement Play() {
        System.Play();
        return this;
    }

    public override ParticleElement destroyoAtEnd() {
        destroyOnTime(System.main.duration);
        return this;
    }

}
