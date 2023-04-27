/** class EffectElement
 * --------------------
 * 
 * Clase que controla los objeto de effectos
 * Tiene en cuanta tanto efectos de Camara, de Canvas y de PostProcesador
 * 
 * @see BasicElement
 * @see effects
 * 
 * @author: Nosink Ð (Ricard Ruiz)
 * @version: v1.0 (04/2023)
 * 
 */

public class EffectElement : BasicElement<EffectElement> {

    /** Effecto */
    private effects _type;

    /** VolumeProfile WIP
    public VolumeProfile EffectProfile {
        get {
            return GetComponent<Volume>().profile;
        }
    } */

    /** Duración */
    public float Duration => _duration;
    private float _duration = float.PositiveInfinity;

    /** Camara WIP
    private GameObject _camera = null;
    public GameObject Camera {
        get {
            if (_camera == null)
                _camera = UnityEngine.Camera.main.gameObject;

            return _camera;
        }
        set {
            _camera = value;
        }
    } */

    /** Método Type
     * Get de _type
     * @return effects Tipo de efecto */
    public effects Type() {
        return _type;
    }

    /** Método Set
     * Estabecle el tipo de efecto y la duración
     * @param effects type Tipo de efecto ya defininod
     * @param float duration Duración del efecto o infinito */
    public EffectElement Set(effects type, float duration = float.PositiveInfinity) {
        _duration = duration;
        _type = type;
        return this;
    }

    /** Método Get<T> WIP
     * Get de los componentes para efectos de post procesador
     * @param T where VolumeComponenet Componente del VolumeProfile
     * @return T Tipo en request 
    public T Get<T>() where T : VolumeComponent {
        T fx = null;
        EffectProfile.TryGet<T>(out fx);
        return fx;
    }*/

    /** Método destoryAtEnd
     * Override del método DestoryAtEnd
     * Destruye el objeto según duración
     * @return EffectElement se devuelve a si mismo */
    public override EffectElement destroyAtEnd() {
        destroyOnTime(Duration);
        return this;
    }

}
