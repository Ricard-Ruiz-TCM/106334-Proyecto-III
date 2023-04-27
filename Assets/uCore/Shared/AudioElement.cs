using System.Collections;
using UnityEngine;

/** class AudioElement
 * -------------------
 * 
 * Clase que controla los objeto de audio
 * Encargado de los efectos de sonido 2D y 3D
 * También controla el Soundtrack y música del juego
 * 
 * @see BasicElement
 * @see audioType
 * 
 * @author: Nosink Ð (Ricard Ruiz)
 * @version: v2.0 (04/2023)
 * 
 */

[RequireComponent(typeof(AudioSource))]
public class AudioElement : BasicElement<AudioElement> {

    /** AudioSource */
    public AudioSource Source {
        get {
            return GetComponent<AudioSource>();
        }
    }

    /** Método Reset
     * Reinicia el audio
     * @return AudioElement Se devuevle a si mismo */
    public AudioElement Reset() {
        Stop();
        Play(Source.volume);
        return this;
    }

    /** Método Play
     * Reproduce un audio con volume o máx volumen
     * @param float volume Volumen del sonido
     * @return AudioElement Se devuevle a si mismo */
    public AudioElement Play(float volume = -1f) {
        Source.volume = (volume == -1f ? Source.volume : volume);
        Source.Play();
        return this;
    }

    /** Método Stop
     * Para el audio
     * @return AudioElement Se devuevle a si mismo */
    public AudioElement Stop() {
        Source.Pause();
        return this;
    }

    /** Método FadeOut
     * Efecto de FadeOut en el sonido
     * @param float time Duración del efecto
     * @param delay Delay del para el inicio del tiempo
     * @param max Volumen máximo que queremos alcanzar
     * @return AudioElement Se devuevle a si mismo */
    public AudioElement FadeOut(float time, float delay = 0f, float min = 0f) {
        if (Source.isPlaying) {
            StartCoroutine(C_FadeAudio(time, delay, Source.volume, min));
        }
        return this;
    }

    /** Método FadeIn
     * Inicia un efecto de FadeIn en el udio
     * @param float time Duración del efecto
     * @param delay Delay del para el inicio del tiempo
     * @param max Volumen máximo que queremos alcanzar
     * @return AudioElement Se devuevle a si mismo */
    public AudioElement FadeIn(float time, float delay = 0f, float max = 1f) {
        if (!Source.isPlaying) {
            Play(0f);
        }
        StartCoroutine(C_FadeAudio(time, delay, Source.volume, max));
        return this;
    }

    /** Método C_FadeAudio
     * Método de Coroutine para controlar el fade del audio
     * @param float time Duración total del efecto
     * @param float delay Delay del efecto
     * @param float start Volumen origen
     * @param float end Volumen destino
     * @return AudioElement Se devuevle a si mismo */
    private IEnumerator C_FadeAudio(float time, float delay, float start, float end) {
        yield return new WaitForSeconds(delay);
        float startTime = Time.time;
        while (Time.time < startTime + time) {
            Source.volume = Mathf.Lerp(start, end, (Time.time - startTime) / time);
            yield return null;
        }
        Source.volume = end;
    }

    /** Método PlayDelayed
     * Reproduce un audio con delay
     * @param float delay Tiempo de delay
     * @return AudioElement Se devuevle a si mismo */
    public AudioElement PlayDelayed(float delay) {
        Source.PlayDelayed(delay);
        return this;
    }

    /** Método PlayScheduled
     * Reproduce un tiempo en un momento concreto
     * @param double time Tiempo en el que se ejeecutara
     * @return AudioElement Se devuevle a si mismo */
    public AudioElement PlayScheduled(double time) {
        Source.PlayScheduled(time);
        return this;
    }

    /** Método Mute
     * Mutea el AudioSource
     * @return AudioElement Se devuevle a si mismo */
    public AudioElement Mute() {
        Source.mute = true;
        return this;
    }

    /** Método UnMute
     * Desmutea el AudioSource
     * @return AudioElement Se devuevle a si mismo */
    public AudioElement UnMute() {
        Source.mute = false;
        return this;
    }

    /** Método EnableByPassEffects
     * Habilita los efectos
     * @return AudioElement Se devuevle a si mismo */
    public AudioElement EnableByPassEffects() {
        Source.bypassEffects = true;
        return this;
    }

    /** Método DisableByPassEffects
     * Desabilita los effectdos
     * @return AudioElement Se devuevle a si mismo */
    public AudioElement DisableByPassEffects() {
        Source.bypassEffects = false;
        return this;
    }

    /** Método looped
     * Asiga el valor de loop al AudioSource
     * @return AudioElement Se devuevle a si mismo */
    public AudioElement looped() {
        Source.loop = true;
        return this;
    }

    /** Método noLoop
     * Deshabilita la opción de lopear del AudioSource
     * @return AudioElement Se devuevle a si mismo */
    public AudioElement noLoop() {
        Source.loop = false;
        return this;
    }

    /** Método destroyAtEnd
     * Override de destroyAtEnd, destruye el audio al final de su reproducción
     * @return AudioElement Se devuevle a si mismo */
    public override AudioElement destroyAtEnd() {
        destroyOnTime(Source.clip.length - Source.time);
        return this;
    }

    /** Método withVolumeEq
     * Establece el volumen del audio
     * @param float volume Volumen
     * @return AudioElement Se devuevle a si mismo */
    public AudioElement withVolumeEq(float volume) {
        Source.volume = volume;
        return this;
    }

    /** Método withPitchEq
     * Establece el pitch al audio
     * @param float value Valor de pitch
     * @return AudioElement Se devuevle a si mismo */
    public AudioElement withPitchEq(float value) {
        Source.pitch = value;
        return this;
    }

    /** Método withPanningEq
     * Establece el valor del panning del audio
     * @param float pan Valor de Panning
     * @return AudioElement Se devuevle a si mismo */
    public AudioElement withPanningEq(float pan) {
        Source.panStereo = pan;
        return this;
    }

    /** Método with3D
     * Declara el CustomRollOff a modo 3D
     * @param float value la cantidad de "3D" que es el audio
     * @return AudioElement Se devuevle a si mismo */
    public AudioElement with3D(float value = 1f) {
        Source.spatialBlend = value;
        return this;
    }

    /** Método with2D
     * Declara el CustomRollOff a modo 2D
     * @param float value la cantidad de "2D" que es el audio
     * @return AudioElement Se devuevle a si mismo */
    public AudioElement with2D(float value = 0f) {
        with3D(value);
        return this;
    }

    /** Método onMinMaxDistance
     * Estasblece las distancias minima y máxima que se escucha el audio
     * @param float min Distancia minima
     * @param float max Distancia máxima
     * @return AudioElement Se devuevle a si mismo */
    public AudioElement onMinMaxDistance(float min = 0f, float max = 12f) {
        Source.maxDistance = max;
        Source.minDistance = min;
        return this;
    }

    /** Método rollOfType
     * Configura un audio, asigando el RollOff y las distnacias
     * @param AudioRolloffMode type Tipo de RollOff para la gestión del 2D-3D del audio
     * @param float min Distancia minima
     * @param float max Distancia máxima
     * @return AudioElement Se devuevle a si mismo */
    public AudioElement rollOfType(AudioRolloffMode type, float min = 0f, float max = 12f) {
        Source.rolloffMode = type;
        onMinMaxDistance(min, max);
        return this;
    }

    /** Método randomPitch
     * Establece un valor random al pitch
     * @param float min Valor minimo
     * @param float max Valor máximo
     * @return AudioElement Se devuevle a si mismo */
    public AudioElement randomPitch(float min, float max) {
        return withPitchEq(Random.Range(min, max));
    }

    /** Método randomVolume
     * Establece un valor random al volumen
     * @param float min Valor minimo
     * @param float max Valor máximo
     * @return AudioElement Se devuevle a si mismo */
    public AudioElement randomVolume(float min, float max) {
        return withVolumeEq(Random.Range(min, max));
    }

}
