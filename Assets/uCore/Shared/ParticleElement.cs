using UnityEngine;

/** class ParticleElement
 * ----------------------
 * 
 * Clase que controla los objeto de particulas
 * 
 * @see BasicElement
 * 
 * @author: Nosink Ð (Ricard Ruiz)
 * @version: v1.0 (04/2023)
 * 
 */

public class ParticleElement : BasicElement<ParticleElement> {

    /** ParticleSystem */
    public ParticleSystem System {
        get {
            return GetComponent<ParticleSystem>();
        }
    }

    /** Método Play
     * Reproduce el sistema de particulas completo
     * @return ParticleElement se devuelve a si mismo */
    public ParticleElement Play() {
        System.Play();
        return this;
    }

    /** Método destroyAtEnd
     * Override de la destrucción, lo destruye cuando acaba la duración de todo el sistema
     * @return ParticleElement se devuelve a si mismo */
    public override ParticleElement destroyAtEnd() {
        destroyOnTime(System.main.duration);
        return this;
    }

}
